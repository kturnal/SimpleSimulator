using Avalonia.Controls;
using SimpleSimulator.ViewModels;

namespace SimpleSimulator.Views
{
    public partial class MainMenuView : Window
    {
        private MainMenuViewModel _mainViewModel;

        public MainMenuView()
        {
            InitializeComponent(); // initializes the GUI components
            _mainViewModel = new MainMenuViewModel(new MainWindowViewModel());
            DataContext = _mainViewModel;
        }
    }
}