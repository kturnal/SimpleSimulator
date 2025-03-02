using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia.Threading;
using SimpleSimulator;
using SimpleSimulator.Models;
using SimpleSimulator.ViewModels;

namespace SimpleSimulator.ViewModels
{
    public class SimulationViewModel : INotifyPropertyChanged
    {
        private double _speed = 30;
        private double _angle = 60;
        private double _height = 0;
        private string _simulationData = "Ready to simulate.";

        const double CoordinateSystemStartY = 50;   // Space from the left for the Y-axis
        const double CoordinateSystemStartX = 595;   // Bottom of the canvas in pixels
        const double ScaleFactor = 50;     // Scaling factor (meters to pixels)

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

        public ObservableCollection<AxisLabel> XAxisLabels { get; set; } = [];
        public ObservableCollection<AxisLabel> YAxisLabels { get; set; } = [];

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SimulationViewModel()
        {
            GenerateAxisLabels();
        }

        private void GenerateAxisLabels()
        {
            Console.WriteLine("[GenerateAxisLabels] Start generating axis labels..");
            XAxisLabels.Clear();
            YAxisLabels.Clear();
            double xAxisLabelStartPos = CoordinateSystemStartY -5;
            double yAxisLabelStartPos = CoordinateSystemStartX -5;

            // X-Axis Labels (0m to 10m)
            for (int i = 0; i <= 14; i++)
            {
                XAxisLabels.Add(new AxisLabel
                {
                    Label = $"{i*10}",
                    Position = xAxisLabelStartPos + (i * ScaleFactor)
                });
            }

            // Y-Axis Labels (0m to 100m)
            for (int i = 0; i <= 10; i++)
            {
                YAxisLabels.Add(new AxisLabel
                {
                    Label = $"{i*10}",
                    Position = yAxisLabelStartPos - (i * ScaleFactor) -5// Scaling factor (inverted Y-axis)
                });
            }

            OnPropertyChanged(nameof(XAxisLabels));
            OnPropertyChanged(nameof(YAxisLabels));
        }

        public async Task RunSimulation(Action<double, double> onFrameRendered)
        {
            var projectile = new ProjectileModel(Speed, Angle, Height);
            double timeStep = 0.05;
            double time = 0;
            double smallScaleFactor = ScaleFactor / 10;

            while (!projectile.HasHitGround(time))
            {
                double x = CoordinateSystemStartY + (projectile.GetXPosition(time) * smallScaleFactor);
                double y = CoordinateSystemStartX - (projectile.GetYPosition(time) * smallScaleFactor);

                SimulationData = $"Time: {time:F2}s | X: {x:F2}m | Y: {y:F2}m";
                Console.WriteLine("Data: " + SimulationData);
                onFrameRendered(x, y); 

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