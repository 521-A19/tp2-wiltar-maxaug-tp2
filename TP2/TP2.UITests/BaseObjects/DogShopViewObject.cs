using TP2.Externalization;
using Xamarin.UITest;

namespace TP2.UITests.BaseObjects
{
    public class DogShopViewObject : BasePageObject
    {
        public DogShopViewObject(IApp app) : base(app)
        {
        }
        public void EnterTextEntry(string newText, string entryId)
        {
            App.Tap(entryId);
            App.ClearText();
            App.EnterText(newText);
            App.DismissKeyboard();
        }

        public AddNewDogViewObject OpenAddNewDogPage()
        {
            TapButton(UiText.BUTTON_ADD_NEW_DOG);
            return new AddNewDogViewObject(App);
        }
    }
}
