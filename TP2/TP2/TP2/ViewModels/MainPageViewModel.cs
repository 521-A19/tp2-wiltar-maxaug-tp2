using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TP2.Externalization;
using TP2.Services;

namespace TP2.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private IAuthenticationService _authenticationService;
        private IPageDialogService _dialogService;
        public DelegateCommand SignInCommand => new DelegateCommand(SigninAndChangePage);
        public DelegateCommand GoToDogsListCommand => new DelegateCommand(ChangePage);
        public MainPageViewModel(INavigationService navigationService,
                                 IPageDialogService dialogService,
                                 IAuthenticationService authenticationService)
            : base(navigationService)
        {
            _dialogService = dialogService;
            _authenticationService = authenticationService;
            Title = "Main Page";
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
            }
        }


        private void SigninAndChangePage()
        {
            if (_authenticationService.AuthenticatedUser == null)
            {
                _authenticationService.LogIn(Email, Password);
            }
            if (_authenticationService.IsUserAuthenticated)
            {
                var navigationParameters = new NavigationParameters();
                navigationParameters.Add("data", _authenticationService.AuthenticatedUser);
                NavigationService.NavigateAsync("MainPage/DogsListPage", navigationParameters);
            }
            else
            {
                _dialogService.DisplayAlertAsync(UiText.ERROR, UiText.USER_NOT_FOUND, UiText.OK);
            }
        }

        private void ChangePage()
        {
            //var navigationParameters = new NavigationParameters();
            //navigationParameters.Add("data", _authenticationService.AuthenticatedUser);
            NavigationService.NavigateAsync("MainPage/DogsListPage");
        }
    }
}
