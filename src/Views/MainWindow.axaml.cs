using System;
using Avalonia.Controls;
using SimpleSimulator.ViewModels;
using SimpleSimulator.Helpers;

namespace SimpleSimulator.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var vm = new MainWindowViewModel();
            DataContext = vm;
            if (Resources["MainWindowViewModelProxy"] is BindingProxy proxy)
            {
                proxy.Data = vm;
            }
        }
    }
}