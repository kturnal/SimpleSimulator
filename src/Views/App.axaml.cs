using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Controls;
using SimpleSimulator.ViewModels;
using System;

namespace SimpleSimulator.Views
{
   public partial class App : Application
    {
        public NavigationViewModel _navigationViewModel { get; private set; }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                _navigationViewModel = new NavigationViewModel();

                var contentControl = new ContentControl 
                { 
                    
                    DataContext = _navigationViewModel.CurrentView,
                    Content = _navigationViewModel.CurrentView
                };

                _navigationViewModel.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(NavigationViewModel.CurrentView))
                    {
                        Console.WriteLine($"[DEBUG] Changing view to: {_navigationViewModel.CurrentView.GetType().Name}");
                        contentControl.DataContext = _navigationViewModel.CurrentView;  // ✅ Ensure UI updates when navigating
                        contentControl.Content = _navigationViewModel.CurrentView;
                    }
                };

                desktop.MainWindow = new Window
                {
                    DataContext = _navigationViewModel,
                    Content = contentControl,
                    Width = 1024,
                    Height = 768,
                    Title = "Simple Simulator"
                };

                Console.WriteLine($"[DEBUG] MainWindow DataContext: {desktop.MainWindow.DataContext?.GetType().Name ?? "NULL"}");
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}