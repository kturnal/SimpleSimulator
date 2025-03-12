# SimpleSimulator
A simple simulation tool for math&physics equations using MVVM architecture using .NET and Avalonia. 

## Disclaimer

This project is still WIP. An MVP of the solution will be done after the following implementations: 

## 0.1.0 Features

- Core Features & UI 
    - Add one more simulation (e.g. freefall simulator)
- Improve Simulation UI/UX
    - Improve the layout of the simulation screen.
- Application Testing
    - Increase the test coverage to an initial of 70%.
    - Test on Windows (native .exe) and Linux (via .NET Runtime).
- Deployment & Versioning
    - Update README.md with instructions. 
    - Update CHANGELOG.md with releases.
    - Create a GitHub release for Windows and Linux (for v0.1.0)

# Getting Started

You can download the latest version of the executables (currently v0.0.1) from the Releases section. This includdes executables for Windows and Linux, depending on your environment.

To run the project locally on your machine: 

```
git clone https://github.com/kbaker/SimpleSimulator
cd SimpleSimulator
dotnet build
dotnet run --project src/SimpleSimulator.csproj
```