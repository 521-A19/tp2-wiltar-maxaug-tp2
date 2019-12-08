using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TP2.Models.Entities;
using TP2.Services;
using Xamarin.Forms;

namespace TP2.ViewModels
{
    public class DogDetailViewModel : ViewModelBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IAuthenticationService _authenticationService;
        private ICommand _addSelectedDogToTheShoppingCart;
        private Dog _selectedDog;
        public Dog SelectedDog
        {
            get { return _selectedDog; }
            set
            {
                _selectedDog = value;
                RaisePropertyChanged();
            }
        }
        private bool _isSelectedDogAlreadyInTheShoppingCart = true;

        public DogDetailViewModel(INavigationService navigationService,
                                  IShoppingCartService shoppingCartService,
                                  IAuthenticationService authenticationService)
            : base(navigationService)
        {
            _authenticationService = authenticationService;
            _shoppingCartService = shoppingCartService;
            _addSelectedDogToTheShoppingCart = new Command(
                        execute: () => AddDog(),
                        canExecute: () => _isSelectedDogAlreadyInTheShoppingCart);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("selectedDogData"))
            {
                SelectedDog = parameters["selectedDogData"] as Dog;
                Title = "Voici le chien " + SelectedDog.Name;
            }
            if (_shoppingCartService.Contains(SelectedDog.Id) || !IsUserAuthenticated || IsDogOwnedByAuthenticatedUser())
            {
                ChangeCanExecute();
            }
        }

        private bool IsDogOwnedByAuthenticatedUser()
        {
            return AuthenticatedUser.DogId == SelectedDog.Id;
        }

        private User AuthenticatedUser
        {
            get { return _authenticationService.AuthenticatedUser; }
        }

        private bool IsUserAuthenticated
        {
            get { return _authenticationService.IsUserAuthenticated; }
        }

        private void ChangeCanExecute()
        {
            _isSelectedDogAlreadyInTheShoppingCart = false;
            ((Command)AddSelectedDogToTheShoppingCart).ChangeCanExecute();
        }

        private void AddDog()
        {
            _shoppingCartService.AddDogToTheShoppingCart(SelectedDog);
            ChangeCanExecute();
        }

        public ICommand AddSelectedDogToTheShoppingCart
        {
            get
            {
                return _addSelectedDogToTheShoppingCart;
            }
        }
    }
}