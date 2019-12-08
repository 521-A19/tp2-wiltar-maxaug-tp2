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
        const string ID_NAME_ENTRY = "NameId";
        const string ID_RACE_ENTRY = "RaceId";
        const string ID_SEX_ENTRY = "SexId";
        const string ID_PRICE_ENTRY = "PriceId";
        const string ID_DESCRIPTION_ENTRY = "DescriptionId";
        private AndroidApp app;
        private MainPageViewObject _mainPageViewObject;
        private DogsListViewObject _dogsListViewObject;
        private DogShopViewObject _dogShopViewObject;


        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp.Android
              .ApkFile(@"C:\temp/com.companyname.appname.apk")
              .StartApp();
            _mainPageViewObject = new MainPageViewObject(app);
            _dogsListViewObject = _mainPageViewObject.UserHasDogSignIn();
        }
        
        [Test]
        public void MainTitleIsDisplayed()
        {
            _dogShopViewObject = _dogsListViewObject.FromMasterDetailPageNavigateTo(UiText.BUTTON_TO_DOG_SHOP_PAGE) as DogShopViewObject;

            Assert.IsTrue(_mainPageViewObject.IsTextDisplayed(UiText.DOG_SHOP_PAGE_MAIN_TITLE));
        }

        [Test]
        public void UserHasOneDog_WhenNavigatedToDogShopPage_DogInformationsShouldBeDisplayed()
        {
            _dogShopViewObject = _dogsListViewObject.FromMasterDetailPageNavigateTo(UiText.BUTTON_TO_DOG_SHOP_PAGE) as DogShopViewObject;

            Assert.IsTrue(_mainPageViewObject.IsTextDisplayed(UiText.ANY_DOG_NAME));
            Assert.IsTrue(_mainPageViewObject.IsTextDisplayed(UiText.ANY_DOG_DESCRIPTION));
            Assert.IsTrue(_mainPageViewObject.IsTextDisplayed(UiText.ANY_DOG_RACE));
        }

        [Test]
        public void UserHasOneDog_SaveChangesButton_ShouldDisplayAlertMessage()
        {
            _dogShopViewObject = _dogsListViewObject.FromMasterDetailPageNavigateTo(UiText.BUTTON_TO_DOG_SHOP_PAGE) as DogShopViewObject;

            _dogShopViewObject.TapButton(UiText.BUTTON_SAVE_CHANGES);

            Assert.IsTrue(_dogShopViewObject.IsTextDisplayed(UiText.SUCCESS));
            Assert.IsTrue(_dogShopViewObject.IsTextDisplayed(UiText.DOG_INFO_MODIFIED));
        }

        [Test]
        public void UserHasOneDog_SaveChangesButton_ShouldModifyMyDogInformationsInDogListPage()
        {
            _dogShopViewObject = _dogsListViewObject.FromMasterDetailPageNavigateTo(UiText.BUTTON_TO_DOG_SHOP_PAGE) as DogShopViewObject;
            const string NEW_NAME = "newName";
            const string NEW_DESCRIPTION = "newDescription";
            const string NEW_RACE = "newRace";
            const string NEW_SEX = "newSex";
            _dogShopViewObject.EnterTextEntry(NEW_NAME, ID_NAME_ENTRY);
            _dogShopViewObject.EnterTextEntry(NEW_DESCRIPTION, ID_DESCRIPTION_ENTRY);
            _dogShopViewObject.EnterTextEntry(NEW_RACE, ID_RACE_ENTRY);
            _dogShopViewObject.EnterTextEntry(NEW_SEX, ID_SEX_ENTRY);

            _dogShopViewObject.TapButton(UiText.BUTTON_SAVE_CHANGES);
            _dogShopViewObject.AlertConfirm();
            _dogsListViewObject = _dogShopViewObject.FromMasterDetailPageNavigateTo(UiText.BUTTON_TO_DOGS_LIST_PAGE) as DogsListViewObject;

            Assert.IsFalse(_dogsListViewObject.IsTextDisplayed(UiText.ANY_DOG_NAME));
            Assert.IsFalse(_dogsListViewObject.IsTextDisplayed(UiText.ANY_DOG_DESCRIPTION));
            Assert.IsTrue(_dogsListViewObject.IsTextDisplayed(NEW_NAME));
            Assert.IsTrue(_dogsListViewObject.IsTextDisplayed(NEW_DESCRIPTION));
            Assert.IsTrue(_dogsListViewObject.IsTextDisplayed(NEW_RACE));
            Assert.IsTrue(_dogsListViewObject.IsTextDisplayed(NEW_SEX));
        }

        [Test]
        public void UserHasNoDog_WhenNavigatedToDogShopPage_ButtonAddNewDogAndMessageAlertShoulBeDisplayed()
        {
            UserProfileViewObject userProfileViewObject = _dogsListViewObject.FromMasterDetailPageNavigateTo(UiText.BUTTON_TO_USER_PROFIL_PAGE) as UserProfileViewObject;
            _dogsListViewObject = userProfileViewObject.TapDeleteDogShop();
           
            _dogShopViewObject = _dogsListViewObject.FromMasterDetailPageNavigateTo(UiText.BUTTON_TO_DOG_SHOP_PAGE) as DogShopViewObject;

            Assert.IsTrue(_dogShopViewObject.IsTextDisplayed(UiText.WARNING));
            Assert.IsTrue(_dogShopViewObject.IsTextDisplayed(UiText.NO_CURRENT_DOG));
            Assert.IsTrue(_dogShopViewObject.IsTextDisplayed(UiText.BUTTON_ADD_NEW_DOG));
        }
    }
}
