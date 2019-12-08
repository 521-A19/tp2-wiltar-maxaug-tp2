using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP2.Externalization;
using TP2.UITests.BaseObjects;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace TP2.UITests
{
    class ShoppingCartViewObjectTests
    {
        private AndroidApp app;
        private MainPageViewObject _mainPageViewObject;
        private DogsListViewObject _dogsListViewObject;
        private ShoppingCartViewObject _shoppingCartViewObject;
        private DogDetailViewObject _dogDetailViewObject;

        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp.Android
              .ApkFile(@"C:/Users/usager/source/repos/tp2-wiltar-maxaug-tp2/TP2/TP2/TP2.Android/bin/Release/com.companyname.appname-Signed.apk")
              .StartApp();
            _mainPageViewObject = new MainPageViewObject(app);
            _dogsListViewObject = _mainPageViewObject.UserHasDogSignIn();
        }

        [Test]
        public void OnConnexion_ShoppingCartShouldBeEmpty()
        {
            _shoppingCartViewObject = _dogsListViewObject.FromMasterDetailPageNavigateTo(UiText.BUTTON_TO_SHOPPING_CART_PAGE) as ShoppingCartViewObject;

            const string EXPECTED_STRING = "Le coût total est 0 $";
            Assert.IsTrue(_shoppingCartViewObject.IsTextDisplayed(EXPECTED_STRING));
            Assert.IsTrue(_shoppingCartViewObject.IsTextDisplayed(UiText.SHOPPING_CART_PAGE_MAIN_TITLE));
        }

        [Test]
        public void ValidCreditCard_BuyShoppingCart_ShouldNavigateToDogsListPage()
        {
            const string DOG_TO_SELECT = UiText.ANY_DOG_NAME;
            _dogDetailViewObject = _dogsListViewObject.OpenDogDetailViewPage(DOG_TO_SELECT);
            _dogDetailViewObject.TapAddDogToTheShoppingCart();
            _shoppingCartViewObject = _dogDetailViewObject.FromMasterDetailPageNavigateTo(UiText.BUTTON_TO_SHOPPING_CART_PAGE) as ShoppingCartViewObject;
            const string VALID_CREDIT_CARD = "1234";

            _shoppingCartViewObject.EnterCreditCard(VALID_CREDIT_CARD);
            _dogsListViewObject = _shoppingCartViewObject.TapBuyShoppingCart();

            Assert.IsTrue(_dogsListViewObject.IsTextDisplayed(UiText.DOGS_LIST_PAGE_MAIN_TITLE));
            Assert.IsTrue(_dogsListViewObject.IsTextDisplayed(UiText.DOGS_LIST_PAGE_MAIN_LABEL));
        }


        [Test]
        public void ValidCreditCard_BuyShoppingCart_ShouldReinitializeTheShoppingCart()
        {
            const string DOG_TO_SELECT = UiText.ANY_DOG_NAME;
            _dogDetailViewObject = _dogsListViewObject.OpenDogDetailViewPage(DOG_TO_SELECT);
            _dogDetailViewObject.TapAddDogToTheShoppingCart();
            _shoppingCartViewObject = _dogDetailViewObject.FromMasterDetailPageNavigateTo(UiText.BUTTON_TO_SHOPPING_CART_PAGE) as ShoppingCartViewObject;
            const string VALID_CREDIT_CARD = "1234";

            _shoppingCartViewObject.EnterCreditCard(VALID_CREDIT_CARD);
            _dogsListViewObject = _shoppingCartViewObject.TapBuyShoppingCart();
            _shoppingCartViewObject = _dogsListViewObject.FromMasterDetailPageNavigateTo(UiText.BUTTON_TO_SHOPPING_CART_PAGE) as ShoppingCartViewObject;

            const string EXPECTED_PRICE = "Le coût total est 0 $";
            Assert.IsTrue(_shoppingCartViewObject.IsTextDisplayed(EXPECTED_PRICE));
            const string NON_EXPECTED_NAME = DOG_TO_SELECT;
            Assert.IsFalse(_shoppingCartViewObject.IsTextDisplayed(NON_EXPECTED_NAME));
        }

        [Test]
        public void DogInShoppingCart_CancelShoppingCart_ShouldReinitializeTheShoppingCart()
        {
            const string DOG_TO_SELECT = UiText.ANY_DOG_NAME;
            _dogDetailViewObject = _dogsListViewObject.OpenDogDetailViewPage(DOG_TO_SELECT);
            _dogDetailViewObject.TapAddDogToTheShoppingCart();
            _shoppingCartViewObject = _dogDetailViewObject.FromMasterDetailPageNavigateTo(UiText.BUTTON_TO_SHOPPING_CART_PAGE) as ShoppingCartViewObject;

            _dogsListViewObject = _shoppingCartViewObject.TapCancelShoppingCart();
            _shoppingCartViewObject = _dogsListViewObject.FromMasterDetailPageNavigateTo(UiText.BUTTON_TO_SHOPPING_CART_PAGE) as ShoppingCartViewObject;

            const string EXPECTED_PRICE = "Le coût total est 0 $";
            Assert.IsTrue(_shoppingCartViewObject.IsTextDisplayed(EXPECTED_PRICE));
            const string NON_EXPECTED_NAME = DOG_TO_SELECT;
            Assert.IsFalse(_shoppingCartViewObject.IsTextDisplayed(NON_EXPECTED_NAME));
        }

        [Test]
        public void DogInShoppingCart_CancelShoppingCart_ShouldNavigateToDogsListPage()
        {
            _shoppingCartViewObject = _dogsListViewObject.FromMasterDetailPageNavigateTo(UiText.BUTTON_TO_SHOPPING_CART_PAGE) as ShoppingCartViewObject;

            _dogsListViewObject = _shoppingCartViewObject.TapCancelShoppingCart();

            Assert.IsTrue(_dogsListViewObject.IsTextDisplayed(UiText.DOGS_LIST_PAGE_MAIN_TITLE));
            Assert.IsTrue(_dogsListViewObject.IsTextDisplayed(UiText.DOGS_LIST_PAGE_MAIN_LABEL));
        }

        [Test]
        public void InvalidCreditCard_BuyShoppingCart_ShouldDisplayAlertMessage()
        {
            const string DOG_TO_SELECT = UiText.ANY_DOG_NAME;
            _dogDetailViewObject = _dogsListViewObject.OpenDogDetailViewPage(DOG_TO_SELECT);
            _dogDetailViewObject.TapAddDogToTheShoppingCart();
            _shoppingCartViewObject = _dogDetailViewObject.FromMasterDetailPageNavigateTo(UiText.BUTTON_TO_SHOPPING_CART_PAGE) as ShoppingCartViewObject;
            const string INVALID_CREDIT_CARD = "123";

            _shoppingCartViewObject.EnterCreditCard(INVALID_CREDIT_CARD);
            _shoppingCartViewObject.TapButton(UiText.BUTTON_BUY_SHOPPING_CART);

            Assert.IsTrue(_shoppingCartViewObject.IsTextDisplayed(UiText.WARNING));
            Assert.IsTrue(_shoppingCartViewObject.IsTextDisplayed(UiText.INVALID_CONFIRMATION_CREDIT_CARD));
            Assert.IsTrue(_shoppingCartViewObject.IsTextDisplayed(UiText.OK));
        }
    }
}
