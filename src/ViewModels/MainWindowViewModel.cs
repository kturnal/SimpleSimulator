using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System;

namespace SimpleSimulator.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private object _currentView = new object();

        public MainMenuViewModel MainMenuVM { get; }

        public SimulationViewModel SimulationVM { get; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public MainWindowViewModel()
        {
            MainMenuVM = new MainMenuViewModel(NavigateToProjectileMotionCommand);
            SimulationVM = new SimulationViewModel(NavigateToMainMenuCommand);
            // Start with Main Menu View
            CurrentView = MainMenuVM;
        }

        public object CurrentView
        {
            get => _currentView;
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    OnPropertyChanged();
                }
            }
        }

        public void NavigateTo(object viewModel)
        {
            CurrentView = viewModel;
        }

        public ICommand NavigateToProjectileMotionCommand => new RelayCommand(() =>
        {
            NavigateTo(SimulationVM);
        });

        public ICommand NavigateToMainMenuCommand => new RelayCommand(() =>
        {
            NavigateTo(MainMenuVM);
        });
    }
}