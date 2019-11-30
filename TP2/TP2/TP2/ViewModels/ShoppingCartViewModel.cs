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
    public class ShoppingCartViewModel : ViewModelBase
    {
        //private readonly IAuthenticationService _authenticationService;
        public ObservableCollection<Dog> DogListInShoppingCart { get; set; }
        private readonly IShoppingCartService _shoppinfCartService;

        public ShoppingCartViewModel(INavigationService navigationService,
                                    IShoppingCartService shoppingCartService)
            : base(navigationService)
        {
            _shoppinfCartService = shoppingCartService;
            /*
            var dogs = shoppingCartService.GetAllDogsFromShoppingCart();
            DogListInShoppingCart.Clear();
            foreach (var dog in dogs)
            {
                DogListInShoppingCart.Add(dog);
            }*/
            Title = "Mon panier";
            var _dogs = shoppingCartService.ShoppingCartDogList;
            DogListInShoppingCart = new ObservableCollection<Dog>(_dogs);
        }

        public override void OnNavigatedTo(INavigationParameters parameters) //Est appelé avant l'affichage de la page
        {
            base.OnNavigatedTo(parameters);
        }
    }
}