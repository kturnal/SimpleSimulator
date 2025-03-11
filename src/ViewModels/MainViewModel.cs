using Avalonia.Controls;
using Avalonia.Threading;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SimpleSimulator.Views;
using SimpleSimulator.Services;

namespace SimpleSimulator.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly NavigationService _navigationService;
        private readonly SimulationViewModel _simulationViewModel;
        public object CurrentView => _navigationService.CurrentView;

        public ICommand OpenProjectileMotionCommand { get; }
        public ICommand GoBackCommand { get; }

        public MainViewModel(NavigationService navigationService, SimulationViewModel simulationViewModel)
        {
            _navigationService = navigationService;
            _simulationViewModel = simulationViewModel;

            // ✅ Listen for changes in the navigation service
            _navigationService.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(NavigationService.CurrentView))
                {
                    OnPropertyChanged(nameof(CurrentView));
                }
            };

            // ✅ Navigate to the Projectile Motion Simulation
            OpenProjectileMotionCommand = new RelayCommand(() =>
            {
                _navigationService.NavigateTo(new ProjectileMotionView(_simulationViewModel));
            });

            // ✅ Go back to the Main Menu
            GoBackCommand = new RelayCommand(() =>
            {
                _navigationService.NavigateTo(new MainMenuView());
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
