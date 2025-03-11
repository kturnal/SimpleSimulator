using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SimpleSimulator.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private object _currentView = new();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public MainWindowViewModel()
        {
            // ✅ Start with Main Menu View
            CurrentView = new MainMenuViewModel(this);
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
    }
}