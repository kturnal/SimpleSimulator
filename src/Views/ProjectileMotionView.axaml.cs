using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using System;
using SimpleSimulator.ViewModels;
using System.Collections.ObjectModel;

namespace SimpleSimulator.Views
{
    public partial class ProjectileMotionView : UserControl
    {
        private Canvas? _canvas;
        public SimulationViewModel _simulationViewModel => (DataContext as SimulationViewModel)!;

        public ProjectileMotionView()
        {
            InitializeComponent();
            _canvas = this.FindControl<Canvas>("SimulationCanvas")!;

            // Manually bind the simulate button since Avalonia doesn't have built-in commands
            var simulateButton = this.FindControl<Button>("SimulateButton");
            if (simulateButton != null)
            {
                simulateButton.Click += OnSimulateClicked;
            }

            AddYAxisLabels();
            AddXAxisLabels();
        }

        // protected override void OnOpened(EventArgs e)
        // {
        //     base.OnOpened(e);

        // }

        private void AddXAxisLabels()
        {
            if (_canvas == null)
            {
                Console.WriteLine("Error: SimulationCanvas is not found.");
                return;
            }

            foreach (var label in _simulationViewModel.XAxisLabels)
            {
                var textBlock = new TextBlock
                {
                    Text = label.Label,
                    FontWeight = FontWeight.Bold
                };

                // Set the left position based on the bound value
                Canvas.SetLeft(textBlock, label.Position);
                // For the X-axis, we use a fixed top value
                Canvas.SetTop(textBlock, 610);

                _canvas.Children.Add(textBlock);
            }
        }

        private void AddYAxisLabels()
        {
            if (_canvas == null)
                return;

            foreach (var label in _simulationViewModel.YAxisLabels)
            {
                var textBlock = new TextBlock
                {
                    Text = label.Label,
                    FontWeight = FontWeight.Bold,
                };

                double leftPos = 32 - (textBlock.Text.Length - 1)*7;
                // For the Y-axis, use a fixed left position. The calculation below is done so that the text is right-aligned with all elements underlying correctly.
                Canvas.SetLeft(textBlock, leftPos);
                // Set the top position from the bound value
                Canvas.SetTop(textBlock, label.Position);
                //Console.WriteLine("Y-Axis Label: " + label.Label + "LeftPos: " +leftPos+" TopPos: " + label.Position);
                _canvas.Children.Add(textBlock);
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