using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia.Threading;
using System;

namespace SimpleSimulator
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

        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task RunSimulation()
        {
            double g = 9.81; // Gravity
            double radians = Math.PI * Angle / 180.0;
            double vx = Speed * Math.Cos(radians);
            double vy = Speed * Math.Sin(radians);
            double timeStep = 0.05;
            double time = 0;

            while (true)
            {
                double x = vx * time;
                double y = Height + vy * time - 0.5 * g * time * time;

                if (y < 0) break; // Stop when projectile hits the ground

                // Update simulation data on the UI thread
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    SimulationData = $"Time: {time:F2}s | X: {x:F2}m | Y: {y:F2}m";
                });

                await Task.Delay(50);
                time += timeStep;
            }

            // Indicate completion
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                SimulationData = "Simulation Complete!";
            });
        }
    }
}