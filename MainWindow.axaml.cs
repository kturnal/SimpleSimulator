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
        private async void OnSimulateClicked(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            _canvas.Children.Clear(); // Clear previous simulation points
            await RunSimulation();
        }

        /// <summary>
        /// Simulate projectile motion through an asnyc task.
        /// </summary>
        /// <param name="speed">Input speed of projectile.</param>
        /// <param name="angle">The initial launching angle of the projectile.</param>
        /// <param name="initialHeight">The initial height of the projectile.</param>
        private async Task RunSimulation()
        {
            var speed = _simulationViewModel.Speed;
            double g = 9.81; // Gravity
            double radians = Math.PI * _simulationViewModel.Angle / 180.0;
            double vx = speed * Math.Cos(radians);
            double vy = speed * Math.Sin(radians);
            double timeStep = 0.05;
            double time = 0;

            //List<Ellipse> projectiles = new List<Ellipse>();

            while (true)
            {
                double x = vx * time;
                double y = _simulationViewModel.Height + vy * time - 0.5 * g * time * time;
                
                if (y < 0) 
                    break; // Stop when projectile hits the ground
                
                var projectile = new Ellipse
                {
                    Width = 5,
                    Height = 5,
                    Fill = Brushes.Red
                };

                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    Canvas.SetLeft(projectile, x * 5); // Scale for visualization
                    Canvas.SetTop(projectile, 300 - (y * 5)); // Invert Y-axis for correct display
                    _canvas.Children.Add(projectile);
                });

                // Update simulation data in ViewModel
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    _simulationViewModel.SimulationData = $"Time: {time:F2}s | X: {x:F2}m | Y: {y:F2}m";
                });

                await Task.Delay(50);
                time += timeStep;
            }

            // Mark simulation as complete
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                _simulationViewModel.SimulationData = "Simulation Complete!";
            });      
        }
    }
}

