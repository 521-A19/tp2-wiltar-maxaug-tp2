using TP2.Externalization;
using Xamarin.UITest;

namespace TP2.UITests.BaseObjects
{
    public class DogDetailViewObject : BasePageObject
    {

        public DogDetailViewObject(IApp app) : base(app)
        {
        }

        public void TapAddDogToTheShoppingCart()
        {
            App.Tap(UiText.BUTTON_ADD_TO_SHOPPING_CART);
        }
    }
}
