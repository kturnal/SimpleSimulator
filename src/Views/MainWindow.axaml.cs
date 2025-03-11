using Avalonia.Controls;
using SimpleSimulator.ViewModels;

namespace SimpleSimulator.Views
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _mainWindowViewModel;

        public MainWindow(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent(); // initializes the GUI components
            _mainWindowViewModel = mainWindowViewModel;
            DataContext = _mainWindowViewModel;
        }

        public MainWindow()
        {
            InitializeComponent();
            _mainWindowViewModel = new MainWindowViewModel();
            DataContext = _mainWindowViewModel;
        }
    }
}