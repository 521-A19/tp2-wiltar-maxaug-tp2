using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP2.UITests.BaseObjects;
using Xamarin.UITest;
using TP2.UITests.Helpers;
using TP2.Externalization;

namespace TP2.UITests.BaseObjects
{
    public class RegisterViewObject : BasePageObject
    {
        const string ID_EMAIL_ENTRY = "EmailId";
        const string ID_PASSWORD_ENTRY = "PasswordId";
        const string ID_CONFIRM_PASSWORD_ENTRY = "ConfirmPasswordId";

        public RegisterViewObject(IApp app) : base(app)
        {
        }

        public MainPageViewObject TapRegister()
        {
            App.Tap(UiText.BUTTON_CONFIRM_REGISTRATION);
            return new MainPageViewObject(App);
        }

        public void EnterEmail(string email)
        {
            App.Tap(ID_EMAIL_ENTRY);
            App.EnterText(email);
        }
        public void EnterPassword(string password)
        {
            App.Tap(ID_PASSWORD_ENTRY);
            App.EnterText(password);
        }

        public void EnterConfirmationPassword(string confirmation)
        {
            App.Tap(ID_CONFIRM_PASSWORD_ENTRY);
            App.EnterText(confirmation);
        }

        /*
        App.Tap(ID_EMAIL_ENTRY);
            App.EnterText("123");
            App.Tap(ID_PASSWORD_ENTRY);
            App.EnterText(ID_PASSWORD_ENTRY, "456");
            App.DismissKeyboard();
            App.Tap(UiText.CONNECTION); // ou x2
            App.WaitForElement(UiText.DOGS_LIST_PAGE_MAIN_LABEL);*/
    }
}
