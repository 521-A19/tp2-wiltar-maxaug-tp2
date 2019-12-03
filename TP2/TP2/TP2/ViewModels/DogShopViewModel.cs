using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using TP2.Externalization;
using TP2.Models.Entities;
using TP2.Services;
using TP2.Views;

namespace TP2.ViewModels
{
    public class DogShopViewModel : ViewModelBase
    {
        public DelegateCommand ModifyDogInformations => new DelegateCommand(ModifyMyDog);
        private IRepository<Dog> _dogRepositoryService;
        private IPageDialogService _pageDialogService;
        private IAuthenticationService _authenticationService;
        //public ObservableCollection<Dog> UserListOfDogs { get; set; }
        private Dog _myDog;
        public Dog MyDog
        {
            get { return _myDog; }
            set
            {
                _myDog = value;
                RaisePropertyChanged();
            }
        }
        public bool UserHasAnyDog
        {
            get { return AuthenticatedUserHasAnyDog(); }
        }
        public DelegateCommand NavigateToAddNewDogPageCommand => new DelegateCommand(NavigateToAddNewDogPage);

        public DogShopViewModel(INavigationService navigationService,
                                IAuthenticationService authenticationService,
                                IPageDialogService pageDialogService,
                                IRepository<Dog> dogRepositoryService)
            : base(navigationService)
        {
            _pageDialogService = pageDialogService;
            _authenticationService = authenticationService;
            _dogRepositoryService = dogRepositoryService;
            Title = "Mon chien en adoption";
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (!AuthenticatedUserHasAnyDog())
            {
                _pageDialogService.DisplayAlertAsync(UiText.WARNING, UiText.NO_CURRENT_DOG, UiText.CONFIRM);
            }
            else
            {
                MyDog = _dogRepositoryService.GetById(_authenticationService.AuthenticatedUser.DogId);
            }
        }

        private bool AuthenticatedUserHasAnyDog()
        {
            if(_authenticationService.AuthenticatedUser.DogId == -1) return false;
            return true;
        }

        private void ModifyMyDog()
        {
            _dogRepositoryService.Update(MyDog);
            _pageDialogService.DisplayAlertAsync(UiText.SUCCESS, UiText.DOG_INFO_MODIFIED, UiText.CONFIRM);
        }

        private async void NavigateToAddNewDogPage()
        {
            await NavigationService.NavigateAsync("CustomMasterDetailPage/NavigationPage/" + nameof(AddNewDogPage));
        }
    }
}