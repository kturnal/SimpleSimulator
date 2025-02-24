using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleSimulator.Models;
using SimpleSimulator.ViewModels;
using SimpleSimulator;

namespace SimpleSimulator.Views
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
            
            await _simulationViewModel.RunSimulation((x, y) =>
            {
                var point = new Ellipse
                {
                    Width = 5,
                    Height = 5,
                    Fill = Brushes.Blue
                };

                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    if (_canvas != null)
                    {
                        Canvas.SetLeft(point, x * 5);
                        Canvas.SetTop(point, 300 - (y * 5));
                        _canvas.Children.Add(point);
                    }
                });
            });
        }
    }
}

