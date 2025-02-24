using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleSimulator
{
    public partial class MainWindow : Window
    {
        private Canvas? _canvas;
        private SimulationViewModel _simulationViewModel;

        public MainWindow()
        {
            InitializeComponent(); // initializes the GUI components
            _simulationViewModel = new SimulationViewModel();
            DataContext = _simulationViewModel;
            _canvas = this.FindControl<Canvas>("SimulationCanvas")!;

            // Manually bind the simulate button since Avalonia doesn't have built-in commands
            var simulateButton = this.FindControl<Button>("SimulateButton");
            if (simulateButton != null)
            {
                simulateButton.Click += OnSimulateClicked;
            }
        }

        /// <summary>
        /// Event that triggers when the simulate button is clicked.
        /// </summary>
        /// <param name="sender">Sender object that subscribes to the event.</param>
        /// <param name="e">Event arguments.</param>
        private async void OnSimulateClicked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (_canvas == null)
            {
                Console.WriteLine("Error: SimulationCanvas is not found.");
                return;
            }
            _canvas.Children.Clear(); // Clear previous simulation points
            await RunSimulation();
        }

        /// <summary>
        /// Simulate projectile motion through an asnyc task.
        /// </summary>
        private async Task RunSimulation()
        {
            if (_simulationViewModel == null || _canvas == null)
            {
                Console.WriteLine("Error: ViewModel or Canvas is not initialized.");
                return;
            }

            await _simulationViewModel.RunSimulation(_canvas);



            var speed = _simulationViewModel.Speed;
            double g = 9.81; // Gravity
            double radians = Math.PI * _simulationViewModel.Angle / 180.0;
            double vx = speed * Math.Cos(radians);
            double vy = speed * Math.Sin(radians);
            double timeStep = 0.05;
            double time = 0;

            //List<Ellipse> projectiles = new List<Ellipse>();

            while (!projectile.HasHitGround(time))
            {
                double x = projectile.GetXPosition(time);
                double y = projectile.GetYPosition(time);

                var point = new Ellipse
                {
                    Width = 5,
                    Height = 5,
                    Fill = Brushes.Red
                };

                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    if (_canvas != null)
                    {
                        Canvas.SetLeft(point, x * 5);
                        Canvas.SetTop(point, 300 - (y * 5));
                        _canvas.Children.Add(point);
                    }
                });

                await Task.Delay(50);
                time += timeStep;
            }
        }
    }
}

