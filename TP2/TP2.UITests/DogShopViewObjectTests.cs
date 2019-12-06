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
    class DogShopViewObjectTests
    {
        private AndroidApp app;
        private MainPageViewObject _mainPageViewObject;
        private DogsListViewObject _dogsListViewObject;
        private BasePageObject _dogShopViewObject;

        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp.Android
              .ApkFile(@"C:/Users/usager/source/repos/tp2-wiltar-maxaug-tp2/TP2/TP2/TP2.Android/bin/Release/com.companyname.appname-Signed.apk")
              .StartApp();
            _mainPageViewObject = new MainPageViewObject(app);
            _dogsListViewObject = _mainPageViewObject.SignIn();
            _dogShopViewObject = _dogsListViewObject.FromMasterDetailPageNavigateTo(UiText.BUTTON_TO_DOG_SHOP_PAGE);
        }
        
        [Test]
        public void MainTitleIsDisplayed()
        {
            Assert.IsTrue(_mainPageViewObject.IsTextDisplayed(UiText.DOG_SHOP_PAGE_MAIN_TITLE));
        }

        [Test]
        public void UserHasOneDog_WhenNavigatedToDogShopPage_DogInformationsShouldBeDisplayed()
        {
            Assert.IsTrue(_mainPageViewObject.IsTextDisplayed(UiText.ANY_DOG_NAME));
            Assert.IsTrue(_mainPageViewObject.IsTextDisplayed(UiText.ANY_DOG_DESCRIPTION));
            Assert.IsTrue(_mainPageViewObject.IsTextDisplayed(UiText.ANY_DOG_RACE));
        }

        [Test]
        public void UserHasNoDog_WhenNavigatedToDogShopPage_ButtonAddNewDogAndMessageAlertShoulBeDisplayed()
        {
            UserProfileViewObject userProfileViewObject = _dogShopViewObject.FromMasterDetailPageNavigateTo(UiText.BUTTON_TO_USER_PROFIL_PAGE) as UserProfileViewObject;
            userProfileViewObject.TapDeleteDogShop();

            _dogShopViewObject = userProfileViewObject.FromMasterDetailPageNavigateTo(UiText.BUTTON_TO_DOG_SHOP_PAGE);

            Assert.IsTrue(_dogShopViewObject.IsTextDisplayed(UiText.WARNING));
            Assert.IsTrue(_dogShopViewObject.IsTextDisplayed(UiText.NO_CURRENT_DOG));
            Assert.IsTrue(_dogShopViewObject.IsTextDisplayed(UiText.BUTTON_ADD_NEW_DOG));
        }
    }
}
