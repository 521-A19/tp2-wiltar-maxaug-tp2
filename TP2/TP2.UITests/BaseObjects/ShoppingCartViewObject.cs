using TP2.Externalization;
using Xamarin.UITest;

namespace TP2.UITests.BaseObjects
{
    public class ShoppingCartViewObject : BasePageObject
    {
        const string ID_CREDIT_CARD_ENTRY = "CreditCardId";
       
        public ShoppingCartViewObject(IApp app) : base(app)
        {
        }

        public DogsListViewObject TapBuyShoppingCart()
        {
            App.Tap(UiText.BUTTON_BUY_SHOPPING_CART);
            return new DogsListViewObject(App);
        }

        public DogsListViewObject TapCancelShoppingCart()
        {
            App.Tap(UiText.BUTTON_CANCEL_SHOPPING_CART);
            return new DogsListViewObject(App);
        }

        public void EnterCreditCard(string creditCard)
        {
            App.Tap(ID_CREDIT_CARD_ENTRY);
            App.EnterText(creditCard);
            App.DismissKeyboard();
        }
    }
}
