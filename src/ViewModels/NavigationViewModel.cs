using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System;

namespace SimpleSimulator.ViewModels
{
    public class NavigationViewModel : INotifyPropertyChanged
    {
        private object _currentView = new object();

        public MainMenuViewModel MainMenuVM { get; }

        public SimulationViewModel SimulationVM { get; }

        private readonly ICommand _navigateToProjectileMotionCommand;
        private readonly ICommand _navigateToMainMenuCommand;

        public ICommand NavigateToProjectileMotionCommand => _navigateToProjectileMotionCommand;
        public ICommand NavigateToMainMenuCommand => _navigateToMainMenuCommand;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public NavigationViewModel()
        {

            MainMenuVM = new MainMenuViewModel(NavigateToProjectileMotionCommand);
            SimulationVM = new SimulationViewModel(NavigateToMainMenuCommand);
            Console.WriteLine($"MainMenuVM assigned? {MainMenuVM != null}");
            Console.WriteLine($"SimulationVM assigned? {SimulationVM != null}");
            Console.WriteLine("Initializing NavigationViewModel...");

            _navigateToProjectileMotionCommand = new RelayCommand(() =>
            {
                Console.WriteLine("Navigating to ProjectileMotionView...");
                NavigateTo(SimulationVM);
            });

            _navigateToMainMenuCommand = new RelayCommand(() =>
            {
                Console.WriteLine("Navigating to MainMenuView...");
                NavigateTo(MainMenuVM);
            });

            Console.WriteLine("Commands initialized.");
Console.WriteLine($"ProjectileMotionCommand assigned? {_navigateToProjectileMotionCommand != null}");
    Console.WriteLine($"MainMenuCommand assigned? {_navigateToMainMenuCommand != null}");
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
                    OnPropertyChanged(nameof(CurrentView));
                }
            }
        }

        public void NavigateTo(object viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel)); 

            _currentView = viewModel;
            OnPropertyChanged(nameof(CurrentView)); 
        }
    }
}