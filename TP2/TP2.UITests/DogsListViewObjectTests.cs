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
    class DogsListViewObjectTests
    {
        private AndroidApp app;
        private DogsListViewObject _dogsListViewObject;

        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp.Android
              .ApkFile(@"C:/DevMobile/tp2-wiltar-maxaug-tp2/TP2/TP2/TP2.Android/bin/Release/com.companyname.appname-Signed.apk")
              .StartApp();
            var mainPageViewObject = new MainPageViewObject(app);
            _dogsListViewObject = mainPageViewObject.OpenDogsListPage();
        }

        [Test]
        public void OnDogsListPage_TitleAndMainLabelTextsAreDisplayed()
        {
            Assert.IsTrue(_dogsListViewObject.IsTextDisplayed(UiText.DOGS_LIST_PAGE_MAIN_TITLE));
            Assert.IsTrue(_dogsListViewObject.IsTextDisplayed(UiText.DOGS_LIST_PAGE_MAIN_LABEL));
        }

        [Test]
        public void OnDogsListPage_DogsInformationsShouldBeDisplayed()
        {
            const string EXPECTED_NAME_DISPLAYED = UiText.ANY_DOG_NAME;
            const string EXPECTED_DESCRIPTION_DISPLAYED = UiText.ANY_DOG_DESCRIPTION;
            const string EXPECTED_RACE_DISPLAYED = UiText.ANY_DOG_RACE;
            const string EXPECTED_SEX_DISPLAYED = UiText.ANY_DOG_SEX;

            Assert.IsTrue(_dogsListViewObject.IsTextDisplayed(EXPECTED_NAME_DISPLAYED));
            Assert.IsTrue(_dogsListViewObject.IsTextDisplayed(EXPECTED_DESCRIPTION_DISPLAYED));
            Assert.IsTrue(_dogsListViewObject.IsTextDisplayed(EXPECTED_RACE_DISPLAYED));
            Assert.IsTrue(_dogsListViewObject.IsTextDisplayed(EXPECTED_SEX_DISPLAYED));
        }

        [Test]
        public void DogDetailClick_ShouldOpenDogDetailView()
        {
            const string DOG_TO_SELECT = UiText.ANY_DOG_NAME;

            var projectDetailViewObject = _dogsListViewObject.OpenDogDetailViewPage(DOG_TO_SELECT);

            const string EXPECTED_NAME_DISPLAYED = UiText.ANY_DOG_NAME;
            const string EXPECTED_DESCRIPTION_DISPLAYED = UiText.ANY_DOG_DESCRIPTION;
            const string EXPECTED_RACE_DISPLAYED = UiText.ANY_DOG_RACE;
            const string EXPECTED_SEX_DISPLAYED = UiText.ANY_DOG_SEX;
            Assert.IsTrue(_dogsListViewObject.IsTextDisplayed(EXPECTED_NAME_DISPLAYED));
            Assert.IsTrue(_dogsListViewObject.IsTextDisplayed(EXPECTED_DESCRIPTION_DISPLAYED));
            Assert.IsTrue(_dogsListViewObject.IsTextDisplayed(EXPECTED_RACE_DISPLAYED));
            Assert.IsTrue(_dogsListViewObject.IsTextDisplayed(EXPECTED_SEX_DISPLAYED));
        }

        [Test]
        public void TrierParRaceChangeLOrdreDesChienEtLeDernierChienEstEnHautDeLaListe()
        {
            //app.Repl();
            _dogsListViewObject.SelectSortType();
            AppResult[] results = app.WaitForElement("Rex");
            Assert.IsTrue(results.Any());
        }
    }
}
