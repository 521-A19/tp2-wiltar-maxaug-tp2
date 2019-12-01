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
                                  IShoppingCartService shoppingCartService)
            : base(navigationService)
        {
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
            if (_shoppingCartService.Contains(SelectedDog.Id))
            {
                ChangeCanExecute();
            }
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