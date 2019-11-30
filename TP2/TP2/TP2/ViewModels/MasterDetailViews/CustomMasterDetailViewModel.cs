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
        public DelegateCommand DeconnectionCommand { get; set; }
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
            DeconnectionCommand = new DelegateCommand(LogOut);
        }

        private void NavigateAsync(string page) //CommandParameter !
        {
            NavigationService.NavigateAsync("CustomMasterDetailPage/NavigationPage/" + page);
            //NavigationService.NavigateAsync(new System.Uri(page, System.UriKind.Absolute));
        }
        private void LogOut()
        {
            _authenticationService.LogOut();
            NavigationService.GoBackToRootAsync();
        }
    }
}
