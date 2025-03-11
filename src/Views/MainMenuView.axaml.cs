using Avalonia.Controls;
using SimpleSimulator.ViewModels;

namespace SimpleSimulator.Views
{
    public partial class MainMenuView : Window
    {
        //private MainViewModel _mainViewModel;

        public MainMenuView()
        {
            InitializeComponent(); // initializes the GUI components
            // _mainViewModel = new MainViewModel();
            // DataContext = _mainViewModel;
        }
    }
}