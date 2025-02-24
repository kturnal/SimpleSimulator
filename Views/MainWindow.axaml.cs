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

            //TODO find a clean way to reset the ellipse points while keeping the coordinate system. 

            
            // List<Control> coordinateElements = new List<Control>();


            // foreach (var child in _canvas.Children)
            // {
            //     if (child is Line || (child is TextBlock tb && (string)tb.Tag == "AxisLabel"))
            //     {
            //         coordinateElements.Add((Control)child);
            //     }
            // }

            // // Clear only the projectile elements
            // _canvas.Children.Clear();

            // // Re-add the coordinate system elements
            // foreach (var element in coordinateElements)
            // {
            //     _canvas.Children.Add(element);
            // }
            
            if (DataContext is SimulationViewModel viewModel)
            {
                await _simulationViewModel.RunSimulation((x, y) =>
                {
                    Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        if (_canvas == null) return;

                        var point = new Ellipse
                        {
                            Width = 5,
                            Height = 5,
                            Fill = Brushes.Blue
                        };
                        
                        Canvas.SetLeft(point, x);
                        Canvas.SetTop(point, y);
                        _canvas.Children.Add(point);
                        
                    });
                });
            }
        }
    }
}

