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
        public Dog _myDog;
        public Dog MyDog
        {
            get { return _myDog; }
            set
            {
                _myDog = value;
                RaisePropertyChanged();
            }
        }
        public bool IsAuthenticated
        {
            get { return _authenticationService.IsUserAuthenticated; }
        }
        public DelegateCommand NavigateToAddNewDogPageCommand => new DelegateCommand(NavigateToAddNewDogPage);

        private bool _isButtonToAddNewDogPageVisible;
        public bool IsButtonToAddNewDogPageVisible
        {
            get { return _isButtonToAddNewDogPageVisible; }
            set
            {
                _isButtonToAddNewDogPageVisible = value;
                RaisePropertyChanged();
            }
        }

        public DogShopViewModel(INavigationService navigationService,
                                IAuthenticationService authenticationService,
                                IPageDialogService pageDialogService,
                                IRepository<Dog> dogRepositoryService)
            : base(navigationService)
        {
            _pageDialogService = pageDialogService;
            _authenticationService = authenticationService;
            _dogRepositoryService = dogRepositoryService;
            IsButtonToAddNewDogPageVisible = false;
            if (!authenticationService.IsUserAuthenticated)
            {
                _pageDialogService.DisplayAlertAsync(UiText.WARNING, UiText.USER_NOT_CONNECTED, UiText.CONFIRM);
            }
            else
            {
                if(!AuthenticatedUserHasAnyDog())
                {
                    IsButtonToAddNewDogPageVisible = true;
                    _pageDialogService.DisplayAlertAsync(UiText.WARNING, UiText.NO_CURRENT_DOG, UiText.CONFIRM);
                }
                else
                {
                    MyDog = dogRepositoryService.GetById(authenticationService.AuthenticatedUser.DogId);
                }
                //var _dogs = authenticationService.AuthenticatedUser.Dog;
                //UserListOfDogs = new ObservableCollection<Dog>(_dogs);
            }
            Title = "Mon chien en adoption";
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
            await NavigationService.NavigateAsync(new System.Uri("/CustomMasterDetailPage/NavigationPage/AddNewDogPage", System.UriKind.Absolute));
        }
    }
}