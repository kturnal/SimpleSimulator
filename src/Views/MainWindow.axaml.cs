using Avalonia.Controls;
using SimpleSimulator.ViewModels;

namespace SimpleSimulator.Views
{
    public partial class MainWindow : Window
    {
        private MainMenuViewModel _mainViewModel;

        public MainWindow(MainMenuViewModel mainViewModel)
        {
            InitializeComponent(); // initializes the GUI components
            _mainViewModel = mainViewModel;
            DataContext = _mainViewModel;
        }

        // Parameterless constructor for Avalonia
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}