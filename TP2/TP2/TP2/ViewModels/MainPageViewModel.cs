using Prism.Commands;
using Prism.Mvvm;
using TP2.Views;
using TP2.Services;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TP2.Externalization;
using Prism.Services;

namespace TP2.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private IAuthenticationService _authenticationService;
        IPageDialogService _dialogService;
        private string _login;
        private string _password;
        public DelegateCommand GoToDogsListCommand => new DelegateCommand(ChangePage);
        public DelegateCommand NavigateToRegisterPageCommand => new DelegateCommand(NavigateToRegisterPage);
        public DelegateCommand AuthentifivationUserCommand => new DelegateCommand(AuthentificationLogin);
        public MainPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            _authenticationService = authenticationService;
            _dialogService = dialogService;
            Title = "Main Page";
        }

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                RaisePropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged();
            }
        }

        private void ChangePage()
        {
            //var navigationParameters = new NavigationParameters();
            //navigationParameters.Add("data", _authenticationService.AuthenticatedUser);
            NavigationService.NavigateAsync("NavigationPage/DogsListPage");
        }

        private async void NavigateToRegisterPage()
        {
            await NavigationService.NavigateAsync("NavigationPage/RegisterPage");
        }

        private async void AuthentificationLogin()
        {

            try
            {
                _authenticationService.LogIn(Login, Password);
                if (_authenticationService.IsUserAuthenticated == true)
                {
                    var navigationParameters = new NavigationParameters();
                    navigationParameters.Add("data", _authenticationService.AuthenticatedUser);
                    await NavigationService.NavigateAsync("/" + nameof(DogsListPage), navigationParameters);
                }
                else
                {
                    await _dialogService.DisplayAlertAsync(UiText.NotValidLogInTitle, UiText.NotValidLogInMessage, "Okay");
                }
            }
            catch
            {
                await _dialogService.DisplayAlertAsync(UiText.ErrorExceptionThrowTitle, UiText.ErrorExceptionThrowMessage, "Okay");
            }
        }
    }
}

