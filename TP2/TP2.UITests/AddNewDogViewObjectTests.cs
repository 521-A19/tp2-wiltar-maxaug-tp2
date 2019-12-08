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
    public class AddNewDogViewObjectTests
    {
        private AndroidApp app;
        private MainPageViewObject _mainPageViewObject;
        private DogsListViewObject _dogsListViewObject;
        private DogShopViewObject _dogShopViewObject;
        private AddNewDogViewObject _addNewDogViewObject;

        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp.Android
              .ApkFile(@"C:/DevMobile/tp2-wiltar-maxaug-tp2/TP2/TP2/TP2.Android/bin/Release/com.companyname.appname-Signed.apk")
              .StartApp();
            _mainPageViewObject = new MainPageViewObject(app);
        }
        
        [Test]
        public void OnAddNewDogPage_TitleIsDisplayed()
        {
            _dogsListViewObject = _mainPageViewObject.UserHasNoDogSignIn();
            _dogShopViewObject = _dogsListViewObject.FromMasterDetailPageNavigateTo(UiText.BUTTON_TO_DOG_SHOP_PAGE) as DogShopViewObject;
            _dogShopViewObject.TapButton(UiText.CONFIRM);
          
            _addNewDogViewObject = _dogShopViewObject.OpenAddNewDogPage();

            Assert.IsTrue(_addNewDogViewObject.IsTextDisplayed(UiText.ADD_NEW_DOG_PAGE_MAIN_TITLE));
        }
        
        //Ce test peut bugger
        [Test]
        public void ConfirmAddNewDog_ShouldNavigateToDogsListAndAddNewDog()
        {
            _dogsListViewObject = _mainPageViewObject.UserHasNoDogSignIn();
            _dogShopViewObject = _dogsListViewObject.FromMasterDetailPageNavigateTo(UiText.BUTTON_TO_DOG_SHOP_PAGE) as DogShopViewObject;
            _dogShopViewObject.TapButton(UiText.CONFIRM);
            _addNewDogViewObject = _dogShopViewObject.OpenAddNewDogPage();

            const string DOG_NEW_NAME = "Donald";
            _addNewDogViewObject.EnterName(DOG_NEW_NAME);
            _addNewDogViewObject.EnterPrice(500);
            _dogsListViewObject = _addNewDogViewObject.AddNewDog();

            const string EXPECTED_RACE = "affenpinscher";
            _dogsListViewObject.Search(EXPECTED_RACE);
            Assert.IsTrue(_dogsListViewObject.IsTextDisplayed(EXPECTED_RACE));
            Assert.IsTrue(_dogsListViewObject.IsTextDisplayed(DOG_NEW_NAME));
        }

        [Test]
        public void InvalidNameAndPrice_ConfirmAddNewDog_ShouldDisplayErrorMessages()
        {
            _dogsListViewObject = _mainPageViewObject.UserHasNoDogSignIn();
            _dogShopViewObject = _dogsListViewObject.FromMasterDetailPageNavigateTo(UiText.BUTTON_TO_DOG_SHOP_PAGE) as DogShopViewObject;
            _dogShopViewObject.TapButton(UiText.CONFIRM);
            _addNewDogViewObject = _dogShopViewObject.OpenAddNewDogPage();

            _addNewDogViewObject.EnterName(null);
            _addNewDogViewObject.EnterPrice(0);
            _addNewDogViewObject.TapButton(UiText.BUTTON_CONFIRM_ADD_NEW_DOG);

            Assert.IsTrue(_dogsListViewObject.IsTextDisplayed(UiText.DOG_NEED_A_GOOD_PRICE));
            Assert.IsTrue(_dogsListViewObject.IsTextDisplayed(UiText.DOG_NEED_A_NAME));
        }
    }
}