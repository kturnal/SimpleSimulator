using Avalonia.Controls;
using SimpleSimulator.ViewModels;

namespace SimpleSimulator.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var vm = new MainWindowViewModel();
            DataContext = vm;
        }
    }
}