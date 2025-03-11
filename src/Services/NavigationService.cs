using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SimpleSimulator.ViewModels;
using SimpleSimulator.Views;

namespace SimpleSimulator.Services
{
    // public enum ViewEnums
    // {
    //     MainMenu,
    //     ProjectileMotion
    // }

    public class NavigationService : INotifyPropertyChanged
    {
        private object _currentView = null!;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainMenuView MainMenuView { get; }

        public NavigationService()
        {
            MainMenuView = new MainMenuView();
            CurrentView = MainMenuView; // ✅ Start with Main Menu
        }

        public void NavigateTo(object view)
        {
            CurrentView = view;
        }

        // public object ConvertEnumToView(ViewEnums viewEnum)
        // {
        //     return viewEnum switch
        //     {
        //         ViewEnums.MainMenu => MainMenuView,
        //         ViewEnums.ProjectileMotion => ProjectileMotionView,
        //         _ => throw new ArgumentException("Invalid view enum")
        //     };
        // }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
