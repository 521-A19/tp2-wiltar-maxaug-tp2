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

        public DogsListViewObject TapDeleteDogShop()
        {
            App.Tap(UiText.BUTTON_DELETE_MY_DOG); 
            return new DogsListViewObject(App);
        }

        public void NavigateToPrifileStartingToDogListPage()
        {
            App.TapCoordinates(100, 100);
            App.Tap("Profile");
        }
    }
}
