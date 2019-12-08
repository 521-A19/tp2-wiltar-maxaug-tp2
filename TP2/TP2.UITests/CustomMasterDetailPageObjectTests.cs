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
    public class CustomMasterDetailPageObjectTests
    {
        private AndroidApp app;
        private MainPageViewObject _mainPageViewObject;

        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp.Android
              .ApkFile(@"C:/Users/usager/source/repos/tp2-wiltar-maxaug-tp2/TP2/TP2/TP2.Android/bin/Release/com.companyname.appname-Signed.apk")
              .StartApp();
            _mainPageViewObject = new MainPageViewObject(app);
        }
        
        [Test]
        public void Connection_NavigateToMainPage()
        {
            _mainPageViewObject = _mainPageViewObject.FromMasterDetailPageNavigateTo(UiText.BUTTON_CONNEXION) as MainPageViewObject;

            Assert.IsTrue(_mainPageViewObject.IsTextDisplayed(UiText.WELCOME_ON_DOGFINDER));
        }

        [Test]
        public void AuthenticatedUser_Deconnection_NavigateToMainPage()
        {
            var dogsListViewPage = _mainPageViewObject.UserHasDogSignIn();

            _mainPageViewObject = dogsListViewPage.FromMasterDetailPageNavigateTo(UiText.BUTTON_DECONNEXION) as MainPageViewObject;

            Assert.IsTrue(_mainPageViewObject.IsTextDisplayed(UiText.WELCOME_ON_DOGFINDER));
        }
    }
}