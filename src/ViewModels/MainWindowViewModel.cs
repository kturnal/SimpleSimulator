using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SimpleSimulator.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private object _currentView;

        public event PropertyChangedEventHandler PropertyChanged;

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

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
