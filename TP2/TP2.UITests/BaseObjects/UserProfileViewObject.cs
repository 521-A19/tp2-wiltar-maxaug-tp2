using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP2.Externalization;
using TP2.UITests.Helpers;
using Xamarin.UITest;

namespace TP2.UITests.BaseObjects
{
    public class UserProfileViewObject : BasePageObject
    {
        const string ID_EMAIL_ENTRY = "NameId";
        const string ID_PASSWORD_ENTRY = "PasswordId";
        public UserProfileViewObject(IApp app) : base(app)
        {
        }

        public bool IsTextDisplayed(string textToFind)
        {
            return UiTestHelpers.IsTextDisplayed(App, textToFind);
        }

        public void NavigateToUserProfilePage()
        {
            App.Tap(ID_EMAIL_ENTRY);
            App.EnterText("123");
            App.Tap(ID_PASSWORD_ENTRY);
            App.EnterText(ID_PASSWORD_ENTRY, "456");
            App.Tap(UiText.CONNECTION);
            App.Tap(UiText.CONNECTION);
            App.WaitForElement(UiText.MAIN_LABEL);
            App.TapCoordinates(100, 100);
            App.Tap("Profile");
        }

        public void DeleteDogShop()
        {
            NavigateToUserProfilePage();
            App.Tap("Supprimer le chien X");
        }

        public void TapDeleteDogShop()
        {
            App.Tap("Supprimer le chien X");
            App.Tap("Supprimer le chien X");
        }

        public void NavigateToPrifileStartingToDogListPage()
        {
            App.TapCoordinates(100, 100);
            App.Tap("Profile");
        }
    }
}
