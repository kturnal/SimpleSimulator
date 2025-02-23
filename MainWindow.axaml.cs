using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
//using UIThread = Avalonia.Threading.Dispatcher.UIThread;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleSimulator
{
    public partial class MainWindow : Window
    {

        private TextBox? speedInput;
        private TextBox? angleInput;
        private TextBox? heightInput;
        private Button? simulateButton;
        private Canvas? canvas;
        private TextBlock? simulationDataLabel;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            speedInput = this.FindControl<TextBox>("SpeedInput")!;
            angleInput = this.FindControl<TextBox>("AngleInput")!;
            heightInput = this.FindControl<TextBox>("HeightInput")!;
            simulateButton = this.FindControl<Button>("SimulateButton")!;
            canvas = this.FindControl<Canvas>("SimulationCanvas")!;
            simulationDataLabel = this.FindControl<TextBlock>("SimulationDataLabel")!;

            //fill with initial values for fast testing
            speedInput.Text = "20";
            angleInput.Text = "60";
            heightInput.Text = "10";
            
            simulateButton.Click += OnSimulateClicked;
        }

        /// <summary>
        /// Event that triggers when the simulate button is clicked.
        /// </summary>
        /// <param name="sender">Sender object that subscribes to the event.</param>
        /// <param name="e">Event arguments.</param>
        private async void OnSimulateClicked(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {

            if (speedInput == null || angleInput == null || heightInput == null || canvas == null)
                return;

            if (double.TryParse(speedInput.Text, out double speed) &&
                double.TryParse(angleInput.Text, out double angle) &&
                double.TryParse(heightInput.Text, out double initialHeight))
            {
                await RunProjectileMotionSimulation(speed, angle, initialHeight);
            }
        }

        /// <summary>
        /// Simulate projectile motion through an asnyc task.
        /// </summary>
        /// <param name="speed">Input speed of projectile.</param>
        /// <param name="angle">The initial launching angle of the projectile.</param>
        /// <param name="initialHeight">The initial height of the projectile.</param>
        private async Task RunProjectileMotionSimulation(double speed, double angle, double initialHeight)
        {
            double g = 9.81; // Gravity
            double radians = Math.PI * angle / 180.0;
            double vx = speed * Math.Cos(radians);
            double vy = speed * Math.Sin(radians);
            double timeStep = 0.05;
            double time = 0;

            List<Ellipse> projectiles = new List<Ellipse>();

            canvas.Children.Clear(); // NRE possibility? 

            while (true)
            {
                double x = vx * time;
                double y = initialHeight + vy * time - 0.5 * g * time * time;
                
                if (y < 0) 
                    break; // Stop when projectile hits the ground
                
                var projectile = new Ellipse
                {
                    Width = 5,
                    Height = 5,
                    Fill = Brushes.Red
                };

                Canvas.SetLeft(projectile, x * 5); // Scale for visualization
                Canvas.SetTop(projectile, 300 - (y * 5)); // Invert Y-axis for correct display
                canvas.Children.Add(projectile);
                projectiles.Add(projectile);

                Avalonia.Threading.Dispatcher.UIThread.Post(() =>
                {
                    simulationDataLabel!.Text = $"Time: {time:F2}s | X: {x:F2}m | Y: {y:F2}m";
                });
                
                await Task.Delay(50);
                time += timeStep;
            }

            Avalonia.Threading.Dispatcher.UIThread.Post(() =>
            {
                simulationDataLabel!.Text = "Simulation Complete!";
            });             
        }
    }
}

