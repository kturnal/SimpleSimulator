using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SimpleSimulator.Views;
using SimpleSimulator.Services;
using SimpleSimulator.ViewModels;

namespace SimpleSimulator
{
    public partial class App : Application
    {
        private NavigationService _navigationService;
        private SimulationViewModel _simulationViewModel;
        private MainMenuViewModel _mainViewModel;
        private MainWindow _mainWindow;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // ✅ Create the shared NavigationService
                _navigationService = new NavigationService();

                _simulationViewModel = new SimulationViewModel(_navigationService);
                
                // ✅ Create the MainViewModel with NavigationService
                _mainViewModel = new MainMenuViewModel(_navigationService, _simulationViewModel);
                
                // ✅ Set up MainWindow and bind the ViewModel
                _mainWindow = new MainWindow(_mainViewModel);

                desktop.MainWindow = _mainWindow;
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}