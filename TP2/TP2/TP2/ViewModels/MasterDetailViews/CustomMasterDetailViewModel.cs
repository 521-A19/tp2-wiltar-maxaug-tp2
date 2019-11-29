using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using TP2.Services;
using TP2.Views;

namespace TP2.ViewModels.MasterDetailViews
{
    public class CustomMasterDetailViewModel : ViewModelBase
    {
        public DelegateCommand<string> OnNavigateCommand { get; set; }
        public DelegateCommand DeconnectionCommand => new DelegateCommand(LogOut);
        public DelegateCommand ShowUserProfileCommand => new DelegateCommand(UserProfile);
        private readonly IAuthenticationService _authenticationService;
        public bool IsAuthenticated
        {
            get { return _authenticationService.IsUserAuthenticated; }
        }

        public CustomMasterDetailViewModel(INavigationService navigationService,
                                            IAuthenticationService authenticationService)
            : base(navigationService)
        {
            _authenticationService = authenticationService;
            OnNavigateCommand = new DelegateCommand<string>(NavigateAsync);
            //DeconnectionCommand = new DelegateCommand(LogOut);
        }

        private async void NavigateAsync(string page) //CommandParameter !
        {
            await NavigationService.NavigateAsync(page);
        }
        private async void LogOut()
        {
            _authenticationService.LogOut();
            await NavigationService.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(MainPage));
        }

        private async void UserProfile()
        {
            await NavigationService.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(UserProfilePage));

        }
    }
}
