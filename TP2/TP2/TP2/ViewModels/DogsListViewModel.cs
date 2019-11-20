using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TP2.Models.Entities;
using TP2.Services;
using TP2.Views;
using Xamarin.Forms;

namespace TP2.ViewModels
{
    public class DogsListViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        public DelegateCommand GoToDogShopCommand => new DelegateCommand(ChangePage);
        public ObservableCollection<Dog> Dogs { get; set; }

        private Dog _selectedDog;
        public Dog SelectedDog
        {
            get { return _selectedDog; }
            set 
            { 
                if (_selectedDog != value)
                {
                    _selectedDog = value;
                    HandleSelectedDog();  //Change de page vers DogDetailPage
                }
            }
        }

        public bool IsAuthenticated
        {
            get { return _authenticationService.IsUserAuthenticated; }
        }

        public DogsListViewModel(INavigationService navigationService,
                                    IRepository<Dog> repositoryService,
                                    IAuthenticationService authenticationService)
            : base(navigationService)
        {
            Title = "Liste de chiens globale";
            var _dogs = repositoryService.GetAll();
            Dogs = new ObservableCollection<Dog>(_dogs);
            _authenticationService = authenticationService;


        }

        private void HandleSelectedDog()
        {
            //Page page = new Page();
            //page.DisplayAlert("Selected item", "Name: " + SelectedDog.Name + " Race: " + SelectedDog.Race, "OK");
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add("selectedDogData", _selectedDog);
            NavigationService.NavigateAsync("DogDetailPage", navigationParameters);
        }

        private void ChangePage()
        {
            NavigationService.NavigateAsync("DogsListPage/" + nameof(DogShopPage));
        }
    }
}