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
    class DogDetailViewObjectTests
    {
        private AndroidApp app;
        private MainPageViewObject _mainPagePageObject;
        private DogsListViewObject _dogsListPageObject;
        private DogDetailViewObject _dogDetailPageObject;

        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp.Android
              .ApkFile(@"C:/Users/usager/source/repos/tp2-wiltar-maxaug-tp2/TP2/TP2/TP2.Android/bin/Release/com.companyname.appname-Signed.apk")
              .StartApp();
            _mainPagePageObject = new MainPageViewObject(app);
        }

        [Test]
        public void MainTitleIsDisplayed()
        {
            _dogsListPageObject = _mainPagePageObject.OpenDogsListPage();

            _dogDetailPageObject = _dogsListPageObject.OpenDogDetailViewPage(UiText.ANY_DOG_NAME);

            Assert.IsTrue(_dogDetailPageObject.IsTextDisplayed(UiText.TITLE_DOG_DETAIL + UiText.ANY_DOG_NAME));
        }

        [Test]
        public void OnDogDetaiPage_DogInformationsAreDisplayed()
        {
            _dogsListPageObject = _mainPagePageObject.OpenDogsListPage();
            const string DOG_TO_SELECT = UiText.ANY_DOG_NAME;

            _dogDetailPageObject = _dogsListPageObject.OpenDogDetailViewPage(DOG_TO_SELECT);

            const string EXPECTED_NAME_DISPLAYED = UiText.ANY_DOG_NAME;
            const string EXPECTED_DESCRIPTION_DISPLAYED = UiText.ANY_DOG_DESCRIPTION;
            const string EXPECTED_RACE_DISPLAYED = UiText.ANY_DOG_RACE;
            const string EXPECTED_SEX_DISPLAYED = UiText.ANY_DOG_SEX;
            Assert.IsTrue(_dogDetailPageObject.IsTextDisplayed(UiText.TITLE_DOG_DETAIL + UiText.ANY_DOG_NAME));
            Assert.IsTrue(_dogDetailPageObject.IsTextDisplayed(EXPECTED_NAME_DISPLAYED));
            Assert.IsTrue(_dogDetailPageObject.IsTextDisplayed(EXPECTED_DESCRIPTION_DISPLAYED));
            Assert.IsTrue(_dogDetailPageObject.IsTextDisplayed(EXPECTED_RACE_DISPLAYED));
            Assert.IsTrue(_dogDetailPageObject.IsTextDisplayed(EXPECTED_SEX_DISPLAYED));
        }

        [Test]
        public void AddToShoppingCart_DogsInformationsAreDisplayedInShoppingCartPage()
        {
            const string DOG_TO_SELECT = UiText.ANY_DOG_NAME;
            _dogsListPageObject = _mainPagePageObject.SignIn();
            _dogDetailPageObject = _dogsListPageObject.OpenDogDetailViewPage(DOG_TO_SELECT);

            _dogDetailPageObject.TapAddDogToTheShoppingCart();
            _dogDetailPageObject.FromMasterDetailPageNavigateTo(UiText.BUTTON_TO_SHOPPING_CART_PAGE);
            
            const string EXPECTED_NAME_DISPLAYED = UiText.ANY_DOG_NAME;
            const string EXPECTED_RACE_DISPLAYED = UiText.ANY_DOG_RACE;
            const string EXPECTED_SEX_DISPLAYED = UiText.ANY_DOG_SEX;
            const string EXPECTED_PRICE_DISPLAYED = "Le coût total est 259.99 $";
            Assert.IsTrue(_dogDetailPageObject.IsTextDisplayed(EXPECTED_PRICE_DISPLAYED));
            Assert.IsTrue(_dogDetailPageObject.IsTextDisplayed(EXPECTED_NAME_DISPLAYED));
            Assert.IsTrue(_dogDetailPageObject.IsTextDisplayed(EXPECTED_RACE_DISPLAYED));
            Assert.IsTrue(_dogDetailPageObject.IsTextDisplayed(EXPECTED_SEX_DISPLAYED));
        }
    }
}
