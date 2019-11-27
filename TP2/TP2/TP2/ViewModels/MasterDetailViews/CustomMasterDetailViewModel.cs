using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.ViewModels.MasterDetailViews
{
    public class CustomMasterDetailViewModel : ViewModelBase
    {
        public DelegateCommand NavigateToDogsListCommand => new DelegateCommand(NavigateToDogsList);
        public CustomMasterDetailViewModel(INavigationService navigationService)
            : base(navigationService)
        {

        }

        private void NavigateToDogsList()
        {
            NavigationService.NavigateAsync("MainPage/DogsListPage");
        }
    }
}
