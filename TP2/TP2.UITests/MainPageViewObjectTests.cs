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
    class ProjectsViewObjectTests
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
        public void WelcomeTextIsDisplayed()
        {
            AppResult[] results = app.WaitForElement(UiText.WELCOME_ON_DOGFINDER);
            //app.Screenshot("Welcome screen.");
            Assert.IsTrue(results.Any());
        }

        [Test]
        public void OnClickGoToDogList_ShouldNavigateToDogsListPage()
        {
            var mainPageViewObject = new MainPageViewObject(app);
            var dogsListViewObject = mainPageViewObject.OpenDogsListPage();

            AppResult[] results = app.WaitForElement(UiText.MAIN_LABEL);

            Assert.IsTrue(results.Any());
        }

        [Test]
        public void OnClickSignUp_ShouldNavigateRegisterPage()
        {
            var mainPageViewObject = new MainPageViewObject(app);
            
            mainPageViewObject.ClickSignUpButton();

            AppResult[] results = app.WaitForElement("Email address");
            Assert.IsTrue(results.Any());
        }

        [Test]
        public void ValidLoginAndPassword_OnSignIn_ShouldDogsListPage()
        {
            var mainPageViewObject = new MainPageViewObject(app);
            mainPageViewObject.EnterLogin("123");
            mainPageViewObject.EnterPassword("456");

            mainPageViewObject.ClickSignInButton();

            AppResult[] results = app.WaitForElement(UiText.MAIN_LABEL);
            Assert.IsTrue(results.Any());
        }
    }
}
