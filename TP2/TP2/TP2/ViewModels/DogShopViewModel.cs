using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using TP2.Externalization;
using TP2.Models.Entities;
using TP2.Services;

namespace TP2.ViewModels
{
    public class DogShopViewModel : ViewModelBase
    {
        public DelegateCommand GoToDogRegisterPageCommand => new DelegateCommand(ChangePage);
        public DelegateCommand ModifyDogInformations => new DelegateCommand(ChangePage);
        private IRepository<Dog> _dogRepositoryService;
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

        private bool _isButtonToDogRegisterVisible;
        public bool IsButtonToDogRegisterVisible
        {
            get { return _isButtonToDogRegisterVisible; }
            set
            {
                _isButtonToDogRegisterVisible = value;
                RaisePropertyChanged();
            }
        }

        public DogShopViewModel(INavigationService navigationService,
                                IAuthenticationService authenticationService,
                                IPageDialogService pageDialogService,
                                IRepository<Dog> dogRepositoryService)
            : base(navigationService)
        {
            _dogRepositoryService = dogRepositoryService;
            IsButtonToDogRegisterVisible = false;
            if (!authenticationService.IsUserAuthenticated)
            {
                pageDialogService.DisplayAlertAsync(UiText.WARNING, UiText.USER_NOT_CONNECTED, UiText.CONFIRM);
            }
            else
            {
                if(authenticationService.AuthenticatedUser.DogId == -1)
                {
                    IsButtonToDogRegisterVisible = true;
                    pageDialogService.DisplayAlertAsync(UiText.WARNING, UiText.NO_CURRENT_DOG, UiText.CONFIRM);
                }
                else
                {
                    MyDog = dogRepositoryService.GetById(authenticationService.AuthenticatedUser.DogId);
                }
                //var _dogs = authenticationService.AuthenticatedUser.Dog; //chiens de l'user
                //UserListOfDogs = new ObservableCollection<Dog>(_dogs);
            }
            Title = "Mon chien en adoption";
        }


        private void ChangePage()
        {
            //NavigationService.NavigateAsync(new Uri("DogsListPage/DogDetailPage", UriKind.Relative));
        }


        private void ModifyDogOnRepository()
        {
            _dogRepositoryService.Update(MyDog);
        }
    }
}