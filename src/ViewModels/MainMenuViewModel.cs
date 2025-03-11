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
        public ICommand NavigateToProjectileMotionCommand { get; }

        public MainMenuViewModel(ICommand navigateCommand)
        {
            NavigateToProjectileMotionCommand = navigateCommand;
        }
    }
}