using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using TP2.Models.Entities;
using TP2.Services;

namespace TP2.ViewModels
{
    public class DogShopViewModel : ViewModelBase
    {
        public DelegateCommand GoToCommand => new DelegateCommand(ChangePage);
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

        public DogShopViewModel(INavigationService navigationService,
                                IAuthenticationService authenticationService,
                                IPageDialogService pageDialogService,
                                IRepository<Dog> dogRepositoryService)
            : base(navigationService)
        {
            if (!authenticationService.IsUserAuthenticated)
            {
                pageDialogService.DisplayAlertAsync("Attention", "Vous devez être connecté pour placer en adoption votre chien", "D'accord");
            }
            else
            {
                if(authenticationService.AuthenticatedUser.DogId == -1)
                {
                    pageDialogService.DisplayAlertAsync("Aucun chien en adoption", "Cliquez sur placer un chien en adoption", "D'accord");
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
    }
}