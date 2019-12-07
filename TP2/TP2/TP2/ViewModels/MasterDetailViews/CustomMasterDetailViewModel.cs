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
        private readonly IShoppingCartService _shoppingCartService;
        public bool IsAuthenticated
        {
            get { return _authenticationService.IsUserAuthenticated; }
        }

        public CustomMasterDetailViewModel(INavigationService navigationService,
                                            IAuthenticationService authenticationService,
                                            IShoppingCartService shoppingCartService)
            : base(navigationService)
        {
            _authenticationService = authenticationService;
            _shoppingCartService = shoppingCartService;
            OnNavigateCommand = new DelegateCommand<string>(NavigateAsync);
            DeconnectionCommand = new DelegateCommand(LogOut);
        }

        private async void NavigateAsync(string page) //page from CommandParameter !
        {
            await NavigationService.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + page);
            //NavigationService.NavigateAsync(new System.Uri(page, System.UriKind.Absolute));
        }
        private async void LogOut()
        {
            _authenticationService.LogOut();                //Vide le User
            _shoppingCartService.SetNewEmptyShoppingCart(); //Vide la liste et le price
            await NavigationService.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(MainPage));
        }
    }
}
