using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP2.Externalization;
using Xamarin.UITest;

namespace TP2.UITests.BaseObjects
{
    public class AddNewDogViewObject : BasePageObject
    {
        const string ID_NAME_ENTRY = "NameId";
        const string ID_PRICE_ENTRY = "PriceId";

        public AddNewDogViewObject(IApp app) : base(app)
        {
        }

        public DogsListViewObject AddNewDog()
        {
            App.Tap(UiText.BUTTON_CONFIRM_ADD_NEW_DOG);
            return new DogsListViewObject(App);
        }

        public void EnterName(string name)
        {
            App.WaitForElement(ID_NAME_ENTRY);
            App.EnterText(ID_NAME_ENTRY, name);
            App.DismissKeyboard();
        }

        public void EnterPrice(float price)
        {
            App.WaitForElement(ID_PRICE_ENTRY);
            App.EnterText(ID_PRICE_ENTRY, price.ToString());
            App.DismissKeyboard();
        }
    }
}
