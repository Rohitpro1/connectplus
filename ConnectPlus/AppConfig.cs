using System;
using System.IO;

namespace ConnectPlus
{
    public static class AppConfig
    {
        // LibVLC native library path (user must set manually)
        // Example: @"C:\LibVLC\x64" or @"C:\Program Files\VideoLAN\VLC"
        public static string LibVlcPath { get; set; } = @"C:\Program Files\VideoLAN\VLC";

        // FFmpeg executable path (user must set manually)
        // Example: @"C:\ffmpeg\bin\ffmpeg.exe"
        public static string FfmpegPath { get; set; } = @"C:\ffmpeg-8.0.1-essentials_build\ffmpeg-8.0.1-essentials_build\bin\ffmpeg.exe";

        // Whisper executable path (optional for MVP)
        public static string WhisperPath { get; set; } = @"C:\whisper\whisper.exe";
        public static string WhisperModelPath { get; set; } = @"C:\whisper\models\ggml-small.en.bin";


        // Ollama API URL
        public static string OllamaUrl { get; set; } = "http://localhost:11434";

        // App data directory
        public static string AppDataDir => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "ConnectPlus"
        );

        // Activation file path
        public static string ActivationFilePath => Path.Combine(AppDataDir, "connectplus_activation.json");

        // Ensure app data directory exists
        public static void EnsureAppDataDir()
        {
            if (!Directory.Exists(AppDataDir))
            {
                Directory.CreateDirectory(AppDataDir);
            }
        }
    }
}

