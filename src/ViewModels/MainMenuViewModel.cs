using System.Windows.Input;

namespace SimpleSimulator.ViewModels
{
    public class MainMenuViewModel
    {
        public ICommand NavigateToProjectileMotionCommand { get; }

        public MainMenuViewModel(ICommand navigateCommand)
        {
            NavigateToProjectileMotionCommand = navigateCommand;
        }
    }
}