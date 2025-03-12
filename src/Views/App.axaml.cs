using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Controls;
using SimpleSimulator.ViewModels;

namespace SimpleSimulator.Views
{
   public partial class App : Application
    {
        public NavigationViewModel _navigationViewModel { get; private set; }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            _navigationViewModel = new NavigationViewModel();
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new Window
                {
                    DataContext = _navigationViewModel,
                    Content = new ContentControl { Content = _navigationViewModel.CurrentView },
                    Width = 1024,
                    Height = 768,
                    Title = "Simple Simulator"
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}