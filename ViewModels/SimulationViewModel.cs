using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia.Threading;
using System;
using SimpleSimulator;
using SimpleSimulator.Models;

namespace SimpleSimulator.ViewModels
{
    public class SimulationViewModel : INotifyPropertyChanged
    {
        private double _speed = 20;
        private double _angle = 60;
        private double _height = 10;
        private string _simulationData = "Ready to simulate.";

        public double Speed
        {
            get => _speed;
            set
            {
                _speed = value;
                OnPropertyChanged();
            }
        }

        public double Angle
        {
            get => _angle;
            set
            {
                _angle = value;
                OnPropertyChanged();
            }
        }

        public double Height
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged();
            }
        }

        public string SimulationData
        {
            get => _simulationData;
            set
            {
                _simulationData = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task RunSimulation(Action<double, double> onFrameRendered)
        {
            var projectile = new ProjectileModel(Speed, Angle, Height);
            double timeStep = 0.05;
            double time = 0;

            while (!projectile.HasHitGround(time))
                {
                    double x = projectile.GetXPosition(time);
                    double y = projectile.GetYPosition(time);

                    // Update UI with simulation data
                    await Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        SimulationData = $"Time: {time:F2}s | X: {x:F2}m | Y: {y:F2}m";
                        onFrameRendered(x, y); // Calls the method to render dots in MainWindow.axaml.cs
                    });

                    await Task.Delay(50); // Wait for 50ms before next update
                    time += timeStep;
                }

                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    SimulationData = "Simulation Complete!";
                });
        }
    }
}