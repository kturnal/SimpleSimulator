using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SimpleSimulator.Views;

namespace SimpleSimulator.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private object _currentView;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public MainWindowViewModel()
        {
            // Start with Main Menu View
            CurrentView = new MainMenuView();
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

        public void NavigateToProjectileSimulation()
        {
            NavigateTo(new SimulationViewModel());
        }

        public void NavigateToMainMenu()
        {
            NavigateTo(new MainMenuView());
        }

        public ICommand NavigateToProjectileMotionCommand => new RelayCommand(() =>
        {
             NavigateTo(new SimulationViewModel());
        });
    }
}