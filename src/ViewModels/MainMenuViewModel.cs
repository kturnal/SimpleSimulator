using Avalonia.Controls;
using Avalonia.Threading;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SimpleSimulator.Views;

namespace SimpleSimulator.ViewModels
{
    public class MainMenuViewModel
    {
        private readonly MainWindowViewModel _mainWindowViewModel;

        public ICommand OpenProjectileMotionCommand { get; }

        public MainMenuViewModel()
        {

        }
    }
}