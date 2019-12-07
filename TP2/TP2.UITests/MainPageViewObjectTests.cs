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
        private MainPageViewObject _mainPageViewObject;
        private DogsListViewObject _dogsListViewObject;

        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp.Android
              .ApkFile(@"C:/DevMobile/tp2-wiltar-maxaug-tp2/TP2/TP2/TP2.Android/bin/Release/com.companyname.appname-Signed.apk")
              .StartApp();
            _mainPageViewObject = new MainPageViewObject(app);
        }

        [Test]
        public void WelcomeTextIsDisplayed()
        {
            Assert.IsTrue(_mainPageViewObject.IsTextDisplayed(UiText.WELCOME_ON_DOGFINDER));
        }

        [Test]
        public void OnClickGoToDogList_ShouldNavigateToDogsListPage()
        {
            _dogsListViewObject = _mainPageViewObject.OpenDogsListPage();

            Assert.IsTrue(_dogsListViewObject.IsTextDisplayed(UiText.DOGS_LIST_PAGE_MAIN_TITLE));
            Assert.IsTrue(_dogsListViewObject.IsTextDisplayed(UiText.DOGS_LIST_PAGE_MAIN_LABEL));
        }

        [Test]
        public void OnClickSignUp_ShouldNavigateRegisterPage()
        {
            RegisterViewObject registerViewObject = _mainPageViewObject.ClickSignUpButton();

            Assert.IsTrue(registerViewObject.IsTextDisplayed(UiText.REGISTER_PAGE_MAIN_TITLE));
        }

        [Test]
        public void ValidLoginAndPassword_OnSignIn_ShouldDogsListPage()
        {
            _dogsListViewObject = _mainPageViewObject.SignIn();

            Assert.IsTrue(_dogsListViewObject.IsTextDisplayed(UiText.DOGS_LIST_PAGE_MAIN_TITLE));
            Assert.IsTrue(_dogsListViewObject.IsTextDisplayed(UiText.DOGS_LIST_PAGE_MAIN_LABEL));
        }

        [Test]
        public void InvalidLoginAndPassword_OnSignIn_ShouldDisplayAlertMessage()
        {
            _mainPageViewObject.TapButton(UiText.CONNECTION);

            Assert.IsTrue(_mainPageViewObject.IsTextDisplayed(UiText.NotValidLogInTitle));
            Assert.IsTrue(_mainPageViewObject.IsTextDisplayed(UiText.NotValidLogInMessage));
        }
    }
}
