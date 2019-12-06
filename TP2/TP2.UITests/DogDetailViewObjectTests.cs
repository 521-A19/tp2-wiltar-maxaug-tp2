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
        private MainPageViewObject _mainPageViewObject;
        private DogsListViewObject _dogsListViewObject;

        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp.Android
              .ApkFile(@"C:/Users/usager/source/repos/tp2-wiltar-maxaug-tp2/TP2/TP2/TP2.Android/bin/Release/com.companyname.appname-Signed.apk")
              .StartApp();
            _mainPageViewObject = new MainPageViewObject(app);
            _mainPageViewObject.SignIn();
            _dogsListViewObject = _mainPageViewObject.OpenDogsListPage();
        }

        [Test]
        public void MainTitleIsDisplayed()
        {
            _mainPageViewObject.FromMasterDetailPageNavigateTo(UiText.TITLE_DOG_DETAIL);

            Assert.IsTrue(_mainPageViewObject.IsTextDisplayed(UiText.DOG_SHOP_PAGE_MAIN_TITLE));
        }

        [Test]
        public void DogInformationsShouldBeDisplayed()
        {
            const string DOG_TO_SELECT = UiText.ANY_DOG_NAME;
            var projectDetailViewObject = _dogsListViewObject.OpenDogDetailViewPage(DOG_TO_SELECT);
            const string EXPECTED_NAME_DISPLAYED = UiText.ANY_DOG_NAME;
            const string EXPECTED_DESCRIPTION_DISPLAYED = UiText.ANY_DOG_DESCRIPTION;
            const float EXPECTED_PRICE_DISPLAYED = UiText.ANY_DOG_PRICE;

            Assert.IsTrue(projectDetailViewObject.IsTextDisplayed(EXPECTED_NAME_DISPLAYED));
            Assert.IsTrue(projectDetailViewObject.IsTextDisplayed(EXPECTED_DESCRIPTION_DISPLAYED));
            Assert.IsTrue(projectDetailViewObject.IsTextDisplayed(EXPECTED_PRICE_DISPLAYED.ToString()));
        }
    }
}
