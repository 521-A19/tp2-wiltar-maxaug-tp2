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
    public class UserProfileViewObjectTests

    {
        private AndroidApp app;
        private MainPageViewObject _mainPageViewObject;
        private DogsListViewObject _dogsListViewObject;
        private UserProfileViewObject _userProfileViewObject;

        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp.Android
              .ApkFile(@"C:/Users/usager/source/repos/tp2-wiltar-maxaug-tp2/TP2/TP2/TP2.Android/bin/Release/com.companyname.appname-Signed.apk")
              .StartApp();
            _mainPageViewObject = new MainPageViewObject(app);
            _dogsListViewObject = _mainPageViewObject.UserHasDogSignIn();
        }

        [Test]
        public void OnUserProfilePage_TitleAndDogsInformationsAreDisplayed()
        {
            _userProfileViewObject = _dogsListViewObject.FromMasterDetailPageNavigateTo(UiText.BUTTON_TO_USER_PROFIL_PAGE) as UserProfileViewObject;

            const string EXPECTED_NAME_DISPLAYED = UiText.ANY_DOG_NAME;
            const string EXPECTED_RACE_DISPLAYED = UiText.ANY_DOG_RACE;
            const string EXPECTED_SEX_DISPLAYED = UiText.ANY_DOG_SEX;
            const string EXPECTED_DESCRIPTION_DISPLAYED = UiText.ANY_DOG_DESCRIPTION;
            Assert.IsTrue(_userProfileViewObject.IsTextDisplayed(UiText.USER_PROFILE_PAGE_MAIN_TITLE));
            Assert.IsTrue(_userProfileViewObject.IsTextDisplayed(EXPECTED_NAME_DISPLAYED));
            Assert.IsTrue(_userProfileViewObject.IsTextDisplayed(EXPECTED_RACE_DISPLAYED));
            Assert.IsTrue(_userProfileViewObject.IsTextDisplayed(EXPECTED_SEX_DISPLAYED));
            Assert.IsTrue(_userProfileViewObject.IsTextDisplayed(EXPECTED_DESCRIPTION_DISPLAYED));
        }

        [Test]
        public void DeleteMyDog_ShouldRemoveMyDogFromUserProfilePage()
        {
            _userProfileViewObject = _dogsListViewObject.FromMasterDetailPageNavigateTo(UiText.BUTTON_TO_USER_PROFIL_PAGE) as UserProfileViewObject;

            _userProfileViewObject.TapDeleteDogShop();

            Assert.IsFalse(_userProfileViewObject.IsTextDisplayed(UiText.ANY_DOG_NAME));
            Assert.IsFalse(_userProfileViewObject.IsTextDisplayed(UiText.ANY_DOG_DESCRIPTION));
        }

        [Test]
        public void DeleteMyDog_ShouldRemoveMyDogFromDogShopPageAndDisplayAddNewDogButton()
        {
            _userProfileViewObject = _dogsListViewObject.FromMasterDetailPageNavigateTo(UiText.BUTTON_TO_USER_PROFIL_PAGE) as UserProfileViewObject;

            _dogsListViewObject = _userProfileViewObject.TapDeleteDogShop();
            DogShopViewObject dogShopViewObject = _dogsListViewObject.FromMasterDetailPageNavigateTo(UiText.BUTTON_TO_DOG_SHOP_PAGE) as DogShopViewObject;

            Assert.IsFalse(dogShopViewObject.IsTextDisplayed(UiText.ANY_DOG_NAME));
            Assert.IsFalse(dogShopViewObject.IsTextDisplayed(UiText.ANY_DOG_DESCRIPTION));
            Assert.IsTrue(dogShopViewObject.IsTextDisplayed(UiText.WARNING));
            Assert.IsTrue(dogShopViewObject.IsTextDisplayed(UiText.NO_CURRENT_DOG));
            Assert.IsTrue(dogShopViewObject.IsTextDisplayed(UiText.BUTTON_ADD_NEW_DOG));
        }
    }
}