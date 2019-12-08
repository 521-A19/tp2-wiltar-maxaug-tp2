using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TP2.Externalization;
using TP2.Models.Entities;
using TP2.Services;
using TP2.Validation;
using TP2.Validation.Rules;
using TP2.Views;
using Xamarin.Forms;

namespace TP2.ViewModels
{
    public class ShoppingCartViewModel : ViewModelBase
    {
        public ObservableCollection<Dog> DogList { get; set; }
        public float TotalPrice { get; set; }
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IPageDialogService _pageDialogService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ISecureStorageService _secureStorageService;
        private readonly ICryptoService _cryptoService;
        private string _creditCard;
        public bool IsShoppingCartEnabled
        {
            get { return !IsShoppingCartEmpty(); }
        }

        public ShoppingCartViewModel(INavigationService navigationService,
                                    IShoppingCartService shoppingCartService,
                                    IPageDialogService pageDialogService,
                                    IAuthenticationService authenticationService,
                                    ISecureStorageService secureStorageService,
                                    ICryptoService cryptoService)
            : base(navigationService)
        {
            _shoppingCartService = shoppingCartService;
            _pageDialogService = pageDialogService;
            _authenticationService = authenticationService;
            _secureStorageService = secureStorageService;
            _cryptoService = cryptoService;
            Title = UiText.SHOPPING_CART_PAGE_MAIN_TITLE;
            var _dogs = shoppingCartService.ShoppingCartDogList;
            DogList = new ObservableCollection<Dog>(_dogs);
            TotalPrice = _shoppingCartService.TotalPrice;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters) //Est appelé avant l'affichage de la page
        {
            base.OnNavigatedTo(parameters);

            var user = _authenticationService.AuthenticatedUser;
            string key = await _secureStorageService.GetUserEncryptionKeyAsync(user);
            var decryptedCard = _cryptoService.Decrypt(user.CreditCard, key);
            _creditCard = decryptedCard;
        }

        private bool IsShoppingCartEmpty()
        {
            if(_shoppingCartService.ShoppingCartDogList.Count == 0) return true;
            return false;
        }
  
        public ICommand DeleteDogFromTheShoppingCartCommand
        {
            get
            {
                return new Command(execute: (item) =>
                {
                    DeleteDogFromTheShoppingCart(item);
                });
            }
        }

        private void DeleteDogFromTheShoppingCart(object item)
        {
            var dog = (item as Dog);
            _shoppingCartService.RemoveDogFromTheShoppingCart(dog);
            NavigationService.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(ShoppingCartPage));
        }

        public ICommand BuyShoppingCartCommand
        {
            get
            {
                return new Command(execute: (item) =>
                {
                    BuyShoppingCart(item);
                },
                canExecute: (item) => !IsShoppingCartEmpty());
            }
        }

        private void BuyShoppingCart(object item)
        {
            var creditCardEntered = (string)item;
            if (creditCardEntered == _creditCard)
            {
                _shoppingCartService.BuyShoppingCart();  //Remove dogs from the repo
                NavigationService.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(DogsListPage));
            }
            else
            {
                _pageDialogService.DisplayAlertAsync(UiText.WARNING, UiText.INVALID_CONFIRMATION_CREDIT_CARD, UiText.OK);
            }
        }

        public ICommand CancelShoppingCartCommand
        {
            get
            {
                return new Command(execute: () =>
                {
                    CancelShoppingCart();
                });
            }
        }

        private void CancelShoppingCart()
        {
            _shoppingCartService.SetNewEmptyShoppingCart();
            NavigationService.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(DogsListPage));
        }
    }
}