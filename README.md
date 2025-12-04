# ConnectPlus - Desktop Video Player MVP

A futuristic Windows Desktop application built with C# WinForms (.NET 8) featuring video playback, virtual camera streaming, and AI integration capabilities.

## Features

- ✨ **Futuristic Neon UI** - Purple and blue dark theme with glow effects
- 🎬 **Video Playback** - Powered by LibVLC for robust video support
- 🔄 **Loop Playback** - Toggle continuous video looping
- 🔊 **Volume Control** - Adjustable volume slider
- 📹 **Virtual Camera** - Stream video to OBS Virtual Camera via FFmpeg
- 🔐 **Local Activation** - Demo activation system with hardware ID validation
- 🤖 **AI Ready** - Placeholder services for Whisper and Ollama integration

## Project Structure

```
ConnectPlus/
 ├─ ConnectPlus.sln              # Solution file
 ├─ ConnectPlus/
 │  ├─ ConnectPlus.csproj        # Project file
 │  ├─ Program.cs                 # Application entry point
 │  ├─ AppConfig.cs               # Configuration settings
 │  ├─ Resources/                 # Application resources
 │  ├─ Forms/                     # WinForms UI
 │  │   ├─ SplashForm             # Splash screen with fade animation
 │  │   ├─ ActivationForm         # License activation window
 │  │   └─ MainForm               # Main video player interface
 │  ├─ Services/                  # Business logic services
 │  │   ├─ HwidService            # Hardware ID generation
 │  │   ├─ LicenseService         # Activation management
 │  │   ├─ VideoService           # LibVLC and FFmpeg wrapper
 │  │   └─ AiService              # AI integration (Whisper/Ollama)
 │  └─ Libs/                      # External libraries documentation
 └─ README.md                     # This file
```

## Prerequisites

- **Visual Studio 2022** (or later) with .NET 8 SDK
- **Windows 10/11** (64-bit recommended)
- **LibVLC** - Video playback library (see [Libs/README_LIBS.md](ConnectPlus/Libs/README_LIBS.md))
- **FFmpeg** - Video streaming tool (see [Libs/README_LIBS.md](ConnectPlus/Libs/README_LIBS.md))
- **OBS Studio** - For virtual camera (optional, for streaming feature)

## Installation & Setup

### 1. Clone or Extract Project

Open the solution file `ConnectPlus.sln` in Visual Studio 2022.

### 2. Install NuGet Packages

The following packages are automatically restored via the `.csproj` file:

- **LibVLCSharp.WinForms** (v3.8.0) - WinForms video player control
- **LibVLCSharp** (v3.8.0) - LibVLC .NET bindings
- **NAudio** (v2.2.1) - Audio processing (for future features)
- **Newtonsoft.Json** (v13.0.3) - JSON serialization

If packages don't restore automatically:
```
Tools → NuGet Package Manager → Package Manager Console
PM> dotnet restore
```

### 3. Configure External Libraries

Edit `ConnectPlus/AppConfig.cs` and set the paths:

```csharp
public static string LibVlcPath { get; set; } = @"C:\Program Files\VideoLAN\VLC";
public static string FfmpegPath { get; set; } = @"C:\ffmpeg\bin\ffmpeg.exe";
```

**Important:** See [Libs/README_LIBS.md](ConnectPlus/Libs/README_LIBS.md) for detailed setup instructions.

### 4. Build the Project

1. Right-click the solution → **Restore NuGet Packages**
2. Build → **Build Solution** (Ctrl+Shift+B)
3. Ensure build succeeds with no errors

### 5. Run the Application

Press **F5** or click **Start** to run the application.

## Usage

### First Launch

1. **Splash Screen** - The app starts with a fade-in splash screen
2. **Activation** - Enter the demo license key: `DEMO-KEY-1234`
3. **Main Form** - After activation, the main video player opens

### Using the Video Player

1. **Open File** - Click "OPEN FILE" to select a video file
2. **Play/Pause** - Control playback with the PLAY and PAUSE buttons
3. **Loop** - Toggle loop mode (button changes from purple to blue when ON)
4. **Volume** - Adjust volume with the slider (0-100%)
5. **Virtual Camera** - Click "START VIRTUAL CAMERA" to stream video to OBS

### Virtual Camera Setup

1. **Start OBS Studio**
2. Go to **Tools → Start Virtual Camera**
3. In ConnectPlus, load a video and click **START VIRTUAL CAMERA**
4. The video will stream to OBS Virtual Camera
5. Use the virtual camera in other applications (Zoom, Teams, etc.)

## Activation

The MVP uses a local activation system:

- **Demo Key:** `DEMO-KEY-1234`
- **Activation File:** Stored in `%APPDATA%\ConnectPlus\connectplus_activation.json`
- **Hardware ID:** Generated from CPU, BIOS, and MAC address

## Development

### Adding New Features

- **Video Processing:** Extend `VideoService.cs`
- **AI Integration:** Implement methods in `AiService.cs`
- **UI Components:** Add forms in `Forms/` directory
- **Configuration:** Update `AppConfig.cs`

### Code Style

- C# 10+ features (nullable reference types enabled)
- Async/await for I/O operations
- Service-based architecture
- WinForms with custom styling

## Troubleshooting

### LibVLC Initialization Error

- Verify `LibVlcPath` in `AppConfig.cs` points to the correct folder
- Ensure `libvlc.dll` exists in that folder
- Check if you're using 64-bit LibVLC for a 64-bit build

### FFmpeg Not Found

- Verify `FfmpegPath` in `AppConfig.cs`
- Test FFmpeg in Command Prompt: `ffmpeg -version`

### Virtual Camera Fails

- Ensure OBS Virtual Camera is running
- Check video file path is valid
- Verify video codec (H.264 recommended)

### Activation Issues

- Check `%APPDATA%\ConnectPlus\` folder exists
- Verify file permissions
- Try deleting `connectplus_activation.json` and reactivating

## Future Enhancements

- [ ] Whisper integration for real-time transcription
- [ ] Ollama integration for AI summaries
- [ ] Multiple video source support
- [ ] Playlist management
- [ ] Video effects and filters
- [ ] Network streaming support
- [ ] Cloud activation system

## License

This is an MVP project. License terms to be determined.

## Support

For issues and questions:
1. Check [Libs/README_LIBS.md](ConnectPlus/Libs/README_LIBS.md) for library setup
2. Verify all paths in `AppConfig.cs`
3. Ensure all prerequisites are installed

---

**Built with:** C# .NET 8, WinForms, LibVLC, FFmpeg

**Theme:** Futuristic Neon Purple + Blue Dark UI

