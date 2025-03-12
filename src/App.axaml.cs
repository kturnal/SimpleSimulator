using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SimpleSimulator.Views;
using SimpleSimulator.ViewModels;

namespace SimpleSimulator
{
   public partial class App : Application
    {
        //private MainWindowViewModel _mainWindowViewModel;
        //private MainWindow _mainWindow;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                //_mainWindowViewModel = new MainWindowViewModel();
                desktop.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}