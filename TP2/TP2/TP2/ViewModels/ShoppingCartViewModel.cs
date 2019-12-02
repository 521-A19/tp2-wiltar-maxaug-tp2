using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TP2.Models.Entities;
using TP2.Services;
using TP2.Views;
using Xamarin.Forms;

namespace TP2.ViewModels
{
    public class ShoppingCartViewModel : ViewModelBase
    {
        public ObservableCollection<Dog> DogList { get; set; }
        public float TotalPrice { get; set; }
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartViewModel(INavigationService navigationService,
                                    IShoppingCartService shoppingCartService)
            : base(navigationService)
        {
            _shoppingCartService = shoppingCartService;
            Title = "Mon panier";
            var _dogs = shoppingCartService.ShoppingCartDogList;
            DogList = new ObservableCollection<Dog>(_dogs);
            TotalPrice = _shoppingCartService.TotalPrice;
        }

        public ICommand DeleteDogFromTheShoppingCartCommand
        {
            get
            {
                return new Command((item) =>
                {
                    var dog = (item as Dog);
                    _shoppingCartService.RemoveDogFromTheShoppingCart(dog);
                    NavigationService.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(ShoppingCartPage));
                });
            }
        }
    }
}