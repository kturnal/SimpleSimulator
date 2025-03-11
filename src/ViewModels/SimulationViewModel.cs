using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia.Threading;
using SimpleSimulator.Models;
using SimpleSimulator.Views;
using System.Windows.Input;

namespace SimpleSimulator.ViewModels
{
    public class SimulationViewModel : INotifyPropertyChanged
    {
        private double _speed = 40;
        private string _speedInput = "40";
        private double _angle = 60;
        private string _angleInput = "60";
        private double _height = 0;
        private string _heightInput = "0";
        private string _simulationData = "Ready to simulate.";

        const double CoordinateSystemStartY = 50;   // Space from the left for the Y-axis
        const double CoordinateSystemStartX = 595;   // Bottom of the canvas in pixels
        const double ScaleFactor = 50;     // Scaling factor (meters to pixels)

        public double Speed => _speed;
        public string SpeedInput
        {
            get => _speedInput;
            set
            {
                _speedInput = value;

                // Convert safely to double, handle empty string
                if (double.TryParse(value, out double parsedSpeed))
                {
                    _speed = parsedSpeed;
                }
                else
                {
                    Console.WriteLine("Invalid input for Speed, setting to 0");
                    _speed = 0; // Default to 0 if conversion fails
                }

                OnPropertyChanged(nameof(Speed));
                OnPropertyChanged(nameof(SpeedInput));
            }
        }

        public double Angle => _angle;
        public string AngleInput
        {
            get => _angleInput;
            set
            {
                _angleInput = value;

                // Convert safely to double, handle empty string
                if (double.TryParse(value, out double parsedAngle))
                {
                    _angle = parsedAngle;
                }
                else
                {
                    Console.WriteLine("Invalid input for Angle, setting to 0");
                    _angle = 0; // Default to 0 if conversion fails
                }

                OnPropertyChanged(nameof(Angle));
                OnPropertyChanged(nameof(AngleInput));
            }
        }
    

        public double Height => _height;
        public string HeightInput
        {
            get => _heightInput;
            set
            {
                _heightInput = value;

                // Convert safely to double, handle empty string
                if (double.TryParse(value, out double parsedHeight))
                {
                    _height = parsedHeight;
                }
                else
                {
                    Console.WriteLine("Invalid input for Height, setting to 0");
                    _height = 0; // Default to 0 if conversion fails
                }

                OnPropertyChanged(nameof(Height));
                OnPropertyChanged(nameof(HeightInput));
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

        private readonly MainWindowViewModel _mainWindowViewModel;

        public ICommand GoBackCommand { get; }

        public SimulationViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;

            GoBackCommand = new RelayCommand(() =>
            {
                _mainWindowViewModel.NavigateTo(new MainMenuViewModel(_mainWindowViewModel));
            });
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
            var projectile = new ProjectileModel(_speed, _angle, _height);
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