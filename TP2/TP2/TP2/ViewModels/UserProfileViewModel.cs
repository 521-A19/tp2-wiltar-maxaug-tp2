using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using TP2.Models.Entities;
using TP2.Services;

namespace TP2.ViewModels
{
    public class UserProfileViewModel : ViewModelBase
    {
        IAuthenticationService _authenticationService;
        IRepository<Dog> _dogRepository;

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
            _authenticationService = authenticationService;
            _dogRepository = dogRepository;
        }
        public User UserLogIn
        {
            get { return _authenticationService.AuthenticatedUser; }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            MyDog = _dogRepository.GetById(UserLogIn.DogId);
            

        }

    }
}
