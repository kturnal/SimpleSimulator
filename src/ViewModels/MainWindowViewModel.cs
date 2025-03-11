using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SimpleSimulator.Views;
using System;

namespace SimpleSimulator.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private object _currentView = new object();

        public MainMenuViewModel MainMenuVM { get; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public MainWindowViewModel()
        {
            // Initialize MainMenuVM
            MainMenuVM = new MainMenuViewModel(NavigateToProjectileMotionCommand);
            // Start with Main Menu View
            CurrentView = MainMenuVM;
        }

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public void NavigateTo(object viewModel)
        {
            CurrentView = viewModel;
        }

        public ICommand NavigateToProjectileMotionCommand => new RelayCommand(() =>
        {
            Console.WriteLine("navigating to proj motion");
            NavigateTo(new SimulationViewModel());
            OnPropertyChanged(nameof(CurrentView));
        });
    }
}