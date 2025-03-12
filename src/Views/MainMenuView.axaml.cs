using Avalonia.Controls;
using SimpleSimulator.ViewModels;
using System;

namespace SimpleSimulator.Views
{
    public partial class MainMenuView : UserControl
    {
        public MainMenuView()
        {
            InitializeComponent();
            Console.WriteLine($"MainMenuView DataContext: {DataContext?.GetType().Name ?? "NULL"}");
        }
    }
}