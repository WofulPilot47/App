# FPS Benchmarker

A professional Windows desktop application for benchmarking and monitoring frame rates (FPS) of your GPU and system rendering performance.

## Features

- **Real-time FPS Monitoring** - Live FPS counter with instant updates
- **Performance Statistics** - Track average, minimum, and maximum FPS
- **Frame Counter** - Monitor total frames rendered during benchmark
- **Visual Graph** - Real-time performance graph visualization
- **Configurable Duration** - Set custom benchmark duration (seconds)
- **Professional UI** - Dark-themed Windows desktop application

## Requirements

- Windows 10 or later
- .NET 6.0 Runtime or later
- 4GB RAM minimum
- DirectX 11 compatible GPU (optional, for GPU benchmarking)

## Installation

1. Download the latest `FPSBenchmarker.exe` from the releases
2. Run the executable
3. No installation required

## Usage

1. Open the FPS Benchmarker application
2. Enter the desired benchmark duration (in seconds)
3. Click "Start Benchmark" to begin
4. Monitor real-time FPS statistics and the performance graph
5. Click "Stop" to end the benchmark at any time
6. Use "Reset" to clear all data and start fresh

## Building from Source

### Requirements
- Visual Studio 2022 or later
- .NET 6.0 SDK or later

### Build Steps

1. Clone the repository
2. Open `FPSBenchmarker.csproj` in Visual Studio
3. Build the project (Ctrl+Shift+B)
4. Run the application (F5)

### Command Line Build

```bash
dotnet build
dotnet run
```

## Output

The application displays:
- **Current FPS** - Frame rate at this moment
- **Average FPS** - Mean FPS across the benchmark
- **Min FPS** - Lowest FPS recorded
- **Max FPS** - Highest FPS recorded
- **Total Frames** - Number of frames rendered

## Technical Details

- Built with **C# and WPF** for Windows desktop
- Uses **DirectX 11** for GPU acceleration
- Real-time rendering loop for accurate FPS measurement
- Optimized for minimal overhead and accurate benchmarking

## License

MIT License - See LICENSE file for details

## Support

For issues, feature requests, or bug reports, please open an issue on GitHub.