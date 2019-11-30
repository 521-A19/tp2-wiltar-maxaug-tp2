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

        public ShoppingCartViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Mon panier";
            //var _dogs = repositoryService.GetAll();
            //DogListInShoppingCart = new ObservableCollection<Dog>(_dogs);
        }

        public override void OnNavigatedTo(INavigationParameters parameters) //Est appelé avant l'affichage de la page
        {
            base.OnNavigatedTo(parameters);
        }
    }
}