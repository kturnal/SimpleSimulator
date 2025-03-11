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

        public MainMenuViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;

            OpenProjectileMotionCommand = new RelayCommand(() =>
            {
                _mainWindowViewModel.NavigateTo(new SimulationViewModel(_mainWindowViewModel));
            });
        }
    }
    // ✅ Reusable ICommand Implementation
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute ?? (() => true);
        }

        public bool CanExecute(object? parameter) => _canExecute();
        public void Execute(object? parameter) => _execute();
        public event EventHandler? CanExecuteChanged;
    }
}