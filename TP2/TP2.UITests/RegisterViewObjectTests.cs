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
    public class RegisterViewObjectTests
    {
        private AndroidApp app;

        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp.Android
              .ApkFile(@"C:/Users/usager/source/repos/tp2-wiltar-maxaug-tp2/TP2/TP2/TP2.Android/bin/Release/com.companyname.appname-Signed.apk")
              .StartApp();
        }

        [Test]
        public void JeVeuxMeRendreALaPageDEnregistration()
        {
            const string REGISTER_BUTTON = "Register";
            var registerViewObject = new RegisterViewObject(app);

            registerViewObject.GoToRegisterPage();

            AppResult[] results = app.WaitForElement(REGISTER_BUTTON);
            Assert.IsTrue(results.Any());
        }

        [Test]
        public void JeVeuxMeRendreALaPageDEnregistrationEtMenregistrerMaisDesMessagesDerreursApparaient()
        {
            const string EXPECTED_EROOR_MESSAGE = UiText.SecondPasswordIsTheSameOfTheFirst;
            var registerViewObject = new RegisterViewObject(app);

            registerViewObject.GoToRegisterPageAndShowErrorMessage();

            AppResult[] results = app.WaitForElement(EXPECTED_EROOR_MESSAGE);
            Assert.IsTrue(results.Any());
        }

    }
}