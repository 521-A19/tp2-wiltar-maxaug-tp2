using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using TP2.Externalization;
using TP2.Models.Entities;
using TP2.Services;
using TP2.Views;

namespace TP2.ViewModels
{
    public class UserProfileViewModel : ViewModelBase
    {
        IAuthenticationService _authenticationService;
        IRepository<Dog> _dogRepository;

        public DelegateCommand DeleteDogShopCommand => new DelegateCommand(DeleteDogShop);

        private bool _isButtonToAddNewDogPageVisible = true;
        public bool IsButtonToAddNewDogPageVisible
        {
            get { return _isButtonToAddNewDogPageVisible; }
            set
            {
                _isButtonToAddNewDogPageVisible = value;
                RaisePropertyChanged();
            }
        }

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


        public UserProfileViewModel(INavigationService navigationService, IAuthenticationService authenticationService, IRepository<Dog> dogRepository)
            : base(navigationService)
        {
            Title = UiText.USER_PROFILE_PAGE_MAIN_TITLE;
            _authenticationService = authenticationService;
            _dogRepository = dogRepository;
            _isButtonToAddNewDogPageVisible = AuthenticatedUserHasAnyDog();
        }
        public User UserLogIn
        {
            get { return _authenticationService.AuthenticatedUser; }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            MyDog = _dogRepository.GetById(UserLogIn.DogId);

        }

        private async void DeleteDogShop()
        {
            _dogRepository.Delete(MyDog);
            UserLogIn.DogId = -1;

            await NavigationService.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(DogsListPage));
        }

        private bool AuthenticatedUserHasAnyDog()
        {
            if (UserLogIn.DogId == -1) return false;
            return true;
        }
    }
}
