using System;
using TP2.Externalization;
using TP2.UITests.Helpers;
using Xamarin.UITest;

namespace TP2.UITests.BaseObjects
{
    public class BasePageObject
    {
        protected IApp App;
        public bool IsDisplayed { get; }

        public BasePageObject(IApp app)
        {
            App = app;
        }

        public bool IsTextDisplayed(string textToFind)
        {
            return UiTestHelpers.IsTextDisplayed(App, textToFind);
        }

        public void TapButton(string buttonName)
        {
            App.Tap(buttonName);
        }

        public BasePageObject FromMasterDetailPageNavigateTo(string urlPage)
        {
            App.TapCoordinates(100, 100);
            BasePageObject viewObject;
            App.Tap(urlPage);
            if (urlPage == UiText.BUTTON_TO_SHOPPING_CART_PAGE)
            {
                return new ShoppingCartViewObject(App);
            }
            if (urlPage == UiText.BUTTON_TO_DOG_SHOP_PAGE)
            {
                return new DogShopViewObject(App);
            }
            if (urlPage == UiText.BUTTON_TO_DOGS_LIST_PAGE)
            {
                return new DogsListViewObject(App);
            }
            if (urlPage == UiText.BUTTON_TO_USER_PROFIL_PAGE)
            {
                return new UserProfileViewObject(App);
            }
            viewObject = null;
            return viewObject;
            
        }
    }
}
