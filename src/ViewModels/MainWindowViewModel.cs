using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SimpleSimulator.Views;

namespace SimpleSimulator.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private object _currentView;

        public MainMenuViewModel MainMenuVM { get; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public MainWindowViewModel()
        {
            // Start with Main Menu View
            CurrentView = new MainMenuViewModel(NavigateToProjectileMotionCommand);
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
             NavigateTo(new SimulationViewModel()); // SimulationViewModel olacak.
        });
    }
}