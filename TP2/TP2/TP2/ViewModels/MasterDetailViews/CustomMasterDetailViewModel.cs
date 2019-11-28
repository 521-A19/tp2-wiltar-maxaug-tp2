using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using TP2.Views;

namespace TP2.ViewModels.MasterDetailViews
{
    public class CustomMasterDetailViewModel : ViewModelBase
    {
        public DelegateCommand<string> OnNavigateCommand { get; set; }
        public CustomMasterDetailViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            OnNavigateCommand = new DelegateCommand<string>(NavigateAsync);
        }

        private void NavigateAsync(string page) //CommandParameter !
        {
            NavigationService.NavigateAsync("NavigationPage/DogShopPage");
        }

        private void NavigateToDogsList()
        {
            NavigationService.NavigateAsync("MainPage/DogsListPage");
        }
    }
}
