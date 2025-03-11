using Avalonia.Controls;
using SimpleSimulator.ViewModels;

namespace SimpleSimulator.Views
{
    public partial class MainWindow : Window
    {
        private MainViewModel _mainViewModel;

        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent(); // initializes the GUI components
            _mainViewModel = mainViewModel;
            DataContext = _mainViewModel;
        }
    }
}