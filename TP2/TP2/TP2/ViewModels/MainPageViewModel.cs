using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP2.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public DelegateCommand GoToDogsListCommand => new DelegateCommand(ChangePage);
        public MainPageViewModel(INavigationService navigationService,
                                 IAuthenticationService authenticationService)
            : base(navigationService)
        {
            Title = "Main Page";
        }

        private void ChangePage()
        {
            //var navigationParameters = new NavigationParameters();
            //navigationParameters.Add("data", _authenticationService.AuthenticatedUser);
            NavigationService.NavigateAsync("MainPage/DogsListPage");
        }
    }
}
