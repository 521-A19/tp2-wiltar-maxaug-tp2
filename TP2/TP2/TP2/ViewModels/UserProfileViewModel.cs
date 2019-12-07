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
        IRepository<User> _userRepository;

        public DelegateCommand DeleteDogShopCommand => new DelegateCommand(DeleteDogShop);

        private bool _isDeleteMyDogButtonVisible = true;
        public bool IsDeleteMyDogButtonVisible
        {
            get { return _isDeleteMyDogButtonVisible; }
            set
            {
                _isDeleteMyDogButtonVisible = value;
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


        public UserProfileViewModel(INavigationService navigationService, IAuthenticationService authenticationService, IRepository<Dog> dogRepository,
                                        IRepository<User> userRepository)
            : base(navigationService)
        {
            Title = UiText.USER_PROFILE_PAGE_MAIN_TITLE;
            _authenticationService = authenticationService;
            _dogRepository = dogRepository;
            _userRepository = userRepository;
            IsDeleteMyDogButtonVisible = AuthenticatedUserHasAnyDog();
        }
        public User GetAuthenticatedUser
        {
            get { return _authenticationService.AuthenticatedUser; }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            MyDog = _dogRepository.GetById(GetAuthenticatedUser.DogId);

        }

        private async void DeleteDogShop()
        {
            _dogRepository.Delete(MyDog);
            GetAuthenticatedUser.DogId = -1;
            _userRepository.Update(GetAuthenticatedUser);
            await NavigationService.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(DogsListPage));
        }

        private bool AuthenticatedUserHasAnyDog()
        {
            if (GetAuthenticatedUser.DogId == -1) return false;
            return true;
        }
    }
}
