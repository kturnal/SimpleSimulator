using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SimpleSimulator.Views;
using SimpleSimulator.ViewModels;

namespace SimpleSimulator
{
    public partial class App : Application
    {
        private MainWindowViewModel? _mainWindowViewModel;
        private MainMenuViewModel? _mainViewModel;
        private SimulationViewModel? _simulationViewModel;
        private MainWindow? _mainWindow;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {

                _mainWindowViewModel = new MainWindowViewModel();

                _simulationViewModel = new SimulationViewModel(_mainWindowViewModel);
                
                // ✅ Create the MainViewModel with NavigationService
                _mainViewModel = new MainMenuViewModel(_mainWindowViewModel);
                
                // ✅ Set up MainWindow and bind the ViewModel
                _mainWindow = new MainWindow(_mainViewModel);

                desktop.MainWindow = _mainWindow;
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}