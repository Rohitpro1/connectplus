using System;
using System.Diagnostics;
using System.IO;
using LibVLCSharp.Shared;

namespace ConnectPlus.Services
{
    public class VideoService : IDisposable
    {
        private readonly LibVLC? _libVLC;
        private readonly MediaPlayer? _mediaPlayer;
        private Process? _ffmpegProcess;
        private string? _currentVideoPath;

        public VideoService(LibVLC libVLC, MediaPlayer mediaPlayer)
        {
            _libVLC = libVLC ?? throw new ArgumentNullException(nameof(libVLC));
            _mediaPlayer = mediaPlayer ?? throw new ArgumentNullException(nameof(mediaPlayer));

            // Wire event with nullable-safe handler
            _mediaPlayer.EndReached += MediaPlayer_EndReached;
        }

        // Current video path (nullable)
        public string? CurrentVideoPath => _currentVideoPath;

        public void LoadVideo(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                throw new FileNotFoundException("Video file not found", path);
            }

            _currentVideoPath = path;

            // Create media WITHOUT using "using" (because it gets disposed too early)
            var media = new Media(_libVLC, path, FromType.FromPath);

            _mediaPlayer.Media = media;

            // STEP 4 — IMPORTANT FOR LOOPING!
            // Reset internal VLC playback state
            _mediaPlayer.Stop();
        }


        public void Play()
        {
            _mediaPlayer?.Play();
        }

        public void Pause()
        {
            _mediaPlayer?.Pause();
        }

        public void SetVolume(int volume)
        {
            if (_mediaPlayer != null)
            {
                _mediaPlayer.Volume = Math.Max(0, Math.Min(100, volume));
            }
        }

        public int GetVolume()
        {
            return _mediaPlayer?.Volume ?? 0;
        }

        public bool IsPlaying => _mediaPlayer?.IsPlaying ?? false;

        // Loop handling controlled via EndReached event and this flag
        public bool LoopEnabled { get; set; } = false;

        private void MediaPlayer_EndReached(object? sender, EventArgs e)
        {
            try
            {
                if (!LoopEnabled || _mediaPlayer == null)
                    return;

                // Delay required; LibVLC cannot restart immediately at EndReached
                Task.Delay(150).ContinueWith(_ =>
                {
                    try
                    {
                        _mediaPlayer.Stop();
                        _mediaPlayer.Play();
                    }
                    catch { }
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Loop error: " + ex);
            }
        }


        /// <summary>
        /// Starts an FFmpeg process that pushes the given video file to a DirectShow device (e.g., OBS Virtual Camera).
        /// </summary>
        /// <param name="videoPath">Path to video file</param>
        /// <param name="ffmpegPath">Full path to ffmpeg.exe</param>
        /// <param name="deviceName">DirectShow device name (default: OBS Virtual Camera)</param>
        /// <returns>The started Process (nullable)</returns>
        public Process? StartVirtualCamera(string videoPath, string ffmpegPath, string deviceName = "OBS Virtual Camera")
        {
            if (_ffmpegProcess != null && !_ffmpegProcess.HasExited)
            {
                throw new InvalidOperationException("Virtual camera is already running.");
            }

            if (string.IsNullOrEmpty(ffmpegPath) || !File.Exists(ffmpegPath))
            {
                throw new FileNotFoundException("FFmpeg executable not found", ffmpegPath);
            }

            if (string.IsNullOrEmpty(videoPath) || !File.Exists(videoPath))
            {
                throw new FileNotFoundException("Video file not found", videoPath);
            }

            // Escape the video path for command line usage
            string escapedPath = $"\"{videoPath}\"";

            // Build FFmpeg arguments
            // -re to read at native frame rate, -pix_fmt yuv420p for compatibility
            // scale can be added if needed
            string arguments = $"-re -i {escapedPath} -pix_fmt yuv420p -vf \"scale=1280:720\" -f dshow \"video={deviceName}\"";

            var startInfo = new ProcessStartInfo
            {
                FileName = ffmpegPath,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            try
            {
                _ffmpegProcess = Process.Start(startInfo);
                if (_ffmpegProcess != null)
                {
                    // Asynchronously read output/errors to avoid deadlocks
                    _ffmpegProcess.BeginOutputReadLine();
                    _ffmpegProcess.BeginErrorReadLine();
                    _ffmpegProcess.ErrorDataReceived += (s, e) =>
                    {
                        if (!string.IsNullOrWhiteSpace(e.Data))
                            File.AppendAllText("ffmpeg_log.txt", e.Data + Environment.NewLine);
                    };

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to start FFmpeg: {ex}");
                throw;
            }

            return _ffmpegProcess;
        }

        public void StopVirtualCamera()
        {
            if (_ffmpegProcess != null)
            {
                try
                {
                    if (!_ffmpegProcess.HasExited)
                    {
                        _ffmpegProcess.Kill(true);
                        _ffmpegProcess.WaitForExit(5000);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error stopping FFmpeg: {ex.Message}");
                }
                finally
                {
                    try { _ffmpegProcess.Dispose(); } catch { }
                    _ffmpegProcess = null;
                }
            }
        }

        public bool IsVirtualCameraRunning => _ffmpegProcess != null && !_ffmpegProcess.HasExited;

        public int? GetVirtualCameraPid => _ffmpegProcess?.Id;

        /// <summary>
        /// Dispose pattern to ensure ffmpeg is stopped and MediaPlayer disposed when service is disposed.
        /// </summary>
        public void Dispose()
        {
            StopVirtualCamera();

            try
            {
                _mediaPlayer?.Dispose();
            }
            catch { /* swallow */ }
        }
    }
}
