# ConnectPlus - External Libraries Setup

This document explains how to set up the required external libraries and tools for ConnectPlus.

## Required Libraries

### 1. LibVLC (VideoLAN)

**Purpose:** Video playback engine

**Setup:**
- Download LibVLC from: https://www.videolan.org/vlc/download-windows.html
- Extract the VLC installation or download the LibVLC SDK
- Update `AppConfig.LibVlcPath` in `AppConfig.cs` to point to the LibVLC native DLLs folder
  - Example: `@"C:\Program Files\VideoLAN\VLC"`
  - Or: `@"C:\LibVLC\x64"` (if you extracted the SDK)

**Note:** The path should contain `libvlc.dll` and `libvlccore.dll`

### 2. FFmpeg

**Purpose:** Streaming video to OBS Virtual Camera

**Setup:**
- Download FFmpeg from: https://ffmpeg.org/download.html
- Extract to a known location (e.g., `C:\ffmpeg`)
- Update `AppConfig.FfmpegPath` in `AppConfig.cs` to point to `ffmpeg.exe`
  - Example: `@"C:\ffmpeg\bin\ffmpeg.exe"`

**Testing:**
- Open Command Prompt and run: `ffmpeg -version`
- Should display FFmpeg version information

### 3. OBS Virtual Camera

**Purpose:** Virtual camera device for streaming

**Setup:**
- Download OBS Studio from: https://obsproject.com/
- Install OBS Studio
- OBS Virtual Camera is included with OBS Studio
- Start OBS Studio and go to: Tools → Start Virtual Camera
- The virtual camera will appear as "OBS Virtual Camera" in FFmpeg

**Note:** The virtual camera must be started in OBS before using it in ConnectPlus

### 4. Whisper (Optional - Future Implementation)

**Purpose:** Speech-to-text transcription

**Setup:**
- Download Whisper from: https://github.com/openai/whisper
- Or use a pre-built executable
- Download a model file (e.g., `medium.en.bin`)
- Update `AppConfig.WhisperPath` in `AppConfig.cs`
  - Example: `@"C:\whisper\whisper.exe"`

**Note:** This is a placeholder for future AI features

### 5. Ollama (Optional - Future Implementation)

**Purpose:** Local LLM for text summarization

**Setup:**
- Download Ollama from: https://ollama.ai/
- Install and start Ollama
- Default URL: `http://localhost:11434`
- Download a model: `ollama pull llama2`

**Note:** This is a placeholder for future AI features

## Configuration Summary

After setting up the libraries, update `AppConfig.cs`:

```csharp
public static string LibVlcPath { get; set; } = @"YOUR_LIBVLC_PATH";
public static string FfmpegPath { get; set; } = @"YOUR_FFMPEG_PATH";
```

## Troubleshooting

### LibVLC Not Found
- Ensure the path points to the folder containing `libvlc.dll`
- Check if you're using 32-bit or 64-bit (should match your project architecture)

### FFmpeg Not Found
- Verify the path to `ffmpeg.exe` is correct
- Test FFmpeg in Command Prompt

### OBS Virtual Camera Not Detected
- Start OBS Studio
- Go to Tools → Start Virtual Camera
- Verify it appears in device list: `ffmpeg -list_devices true -f dshow -i dummy`

### Virtual Camera Streaming Fails
- Ensure OBS Virtual Camera is running
- Check video file path is valid
- Verify video codec is supported (H.264 recommended)

