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
        public RegisterViewObject(IApp app) : base(app)
        {
        }
        public void GoToRegisterPage()
        {
            App.Tap(UiText.SIGN_UP);
        }

        public void GoToRegisterPageAndShowErrorMessage()
        {
            App.Tap(UiText.SIGN_UP);
            App.Tap("Register");
        }
    }
}
