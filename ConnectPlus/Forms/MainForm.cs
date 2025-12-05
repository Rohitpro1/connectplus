using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
using ConnectPlus.Services;

namespace ConnectPlus.Forms
{
    public partial class MainForm : Form
    {
        private LibVLC? _libVLC;
        private MediaPlayer? _mediaPlayer;
        private VideoView? _videoView;
        private VideoService? _videoService;
        private bool _isLoopEnabled = false;

        public MainForm()
        {
            InitializeComponent();
            InitializeLibVLC();
        }

        private void InitializeLibVLC()
        {
            try
            {
                // Ensure AppConfig.LibVlcPath is set to your libvlc folder
                Core.Initialize(AppConfig.LibVlcPath);

                _libVLC = new LibVLC();
                _mediaPlayer = new MediaPlayer(_libVLC);

                // Initialize VideoService with both LibVLC and MediaPlayer
                _videoService = new VideoService(_libVLC, _mediaPlayer);

                _videoView = new VideoView
                {
                    MediaPlayer = _mediaPlayer,
                    Dock = DockStyle.Fill
                };

                // Ensure panelVideo exists in designer
                panelVideo.Controls.Clear();
                panelVideo.Controls.Add(_videoView);

                // Wire any required events (EndReached handled inside VideoService)
                // but we can subscribe for UI updates if needed
                // Example: update play/pause when media stops/plays (optional)
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize LibVLC: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOpenFile_Click(object? sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Video Files|*.mp4;*.avi;*.mkv;*.mov;*.wmv;*.flv|All Files|*.*";
                dialog.Title = "Select Video File";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (_videoService == null)
                        {
                            MessageBox.Show("Video service not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        _videoService.LoadVideo(dialog.FileName);
                        lblStatus.Text = $"Loaded: {Path.GetFileName(dialog.FileName)}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading video: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnPlay_Click(object? sender, EventArgs e)
        {
            _videoService?.Play();
            UpdatePlayPauseButtons();
        }

        private void btnPause_Click(object? sender, EventArgs e)
        {
            _videoService?.Pause();
            UpdatePlayPauseButtons();
        }

        private void UpdatePlayPauseButtons()
        {
            bool isPlaying = _videoService?.IsPlaying ?? false;
            btnPlay.Enabled = !isPlaying;
            btnPause.Enabled = isPlaying;
        }

        private void btnLoop_Click(object? sender, EventArgs e)
        {
            _isLoopEnabled = !_isLoopEnabled;
            if (_videoService != null) {
                _videoService.LoopEnabled = _isLoopEnabled;
                MediaPlayer? mp = typeof(VideoService)
        .GetField("_mediaPlayer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?
        .GetValue(_videoService) as MediaPlayer;



            if (mp != null)
                {
                    mp.EnableKeyInput = false;
                    mp.EnableMouseInput = false;
                }
            }

            if (_isLoopEnabled)
            {
                btnLoop.BackColor = Color.FromArgb(0, 234, 255);
                btnLoop.Text = "LOOP: ON";
            }
            else
            {
                btnLoop.BackColor = Color.FromArgb(123, 47, 247);
                btnLoop.Text = "LOOP: OFF";
            }
        }

        private void trackVolume_Scroll(object? sender, EventArgs e)
        {
            _videoService?.SetVolume(trackVolume.Value);
            lblVolume.Text = $"Volume: {trackVolume.Value}%";
        }

        private void btnStartVirtualCamera_Click(object? sender, EventArgs e)
        {
            if (_videoService == null)
            {
                MessageBox.Show("Video service not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_videoService.IsVirtualCameraRunning)
            {
                // Stop
                _videoService.StopVirtualCamera();
                btnStartVirtualCamera.Text = "START VIRTUAL CAMERA";
                btnStartVirtualCamera.BackColor = Color.FromArgb(123, 47, 247);
                UpdateVirtualCameraStatus();
            }
            else
            {
                if (_mediaPlayer?.Media == null)
                {
                    MessageBox.Show("Please load a video file first.", "No Video", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    // Get the current video path from VideoService (nullable-safe)
                    string? videoPath = _videoService.CurrentVideoPath;

                    if (string.IsNullOrEmpty(videoPath) || !File.Exists(videoPath))
                    {
                        MessageBox.Show("Video file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Start ffmpeg virtual camera process
                    Process? ffmpegProcess = _videoService.StartVirtualCamera(videoPath, AppConfig.FfmpegPath);
                    if (ffmpegProcess != null)
                    {
                        btnStartVirtualCamera.Text = "STOP VIRTUAL CAMERA";
                        btnStartVirtualCamera.BackColor = Color.FromArgb(255, 50, 50);
                        UpdateVirtualCameraStatus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Error starting virtual camera: {ex.Message}\n\nMake sure:\n- FFmpeg path is correct in AppConfig.cs\n- OBS Virtual Camera is installed",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdateVirtualCameraStatus()
        {
            if (_videoService != null && _videoService.IsVirtualCameraRunning)
            {
                int? pid = _videoService.GetVirtualCameraPid;
                lblVirtualCameraStatus.Text = $"Virtual Camera: ON (PID: {pid})";
                lblVirtualCameraStatus.ForeColor = Color.FromArgb(0, 234, 255);
            }
            else
            {
                lblVirtualCameraStatus.Text = "Virtual Camera: OFF";
                lblVirtualCameraStatus.ForeColor = Color.Gray;
            }
        }

        private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            try
            {
                _videoService?.StopVirtualCamera();
            }
            catch { /* ignore */ }

            try
            {
                _mediaPlayer?.Dispose();
            }
            catch { /* ignore */ }

            try
            {
                _libVLC?.Dispose();
            }
            catch { /* ignore */ }
        }

        private void timerStatus_Tick(object? sender, EventArgs e)
        {
            UpdatePlayPauseButtons();
            UpdateVirtualCameraStatus();
        }
    }
}
