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

        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp.Android
              .ApkFile(@"C:/DevMobile/tp2-wiltar-maxaug-tp2/TP2/TP2/TP2.Android/bin/Release/com.companyname.appname-Signed.apk")
              .StartApp();
        }

        [Test]
        public void JeVeuxMeRendreALaPageDuProfile()
        {
            const string EXPECTED_ELEMENT = "Profile";
            var userProfileViewObject = new UserProfileViewObject(app);

            userProfileViewObject.NavigateToUserProfilePage();

            AppResult[] results = app.WaitForElement(EXPECTED_ELEMENT);
            Assert.IsTrue(results.Any());
        }

        [Test]
        public void JeVeuxMeRendreALaProfilePourSupprimerMonChienEnAdoption()
        {

            const string DELETE_DOG_NAME = "Rex";
            var userProfileViewObject = new UserProfileViewObject(app);

            userProfileViewObject.DeleteDogShop();

            bool findElement = userProfileViewObject.IsTextDisplayed(DELETE_DOG_NAME);
            Assert.IsFalse(findElement);
        }

        [Test]
        public void JeVeuxMeRendreALaProfilePourSupprimerMonChienEnAdoptionEtIlNeDevraisPasApparaitreDansMonProfile()
        {

            const string DELETE_DOG_NAME = "Rex";
            var userProfileViewObject = new UserProfileViewObject(app);

            userProfileViewObject.DeleteDogShop();
            userProfileViewObject.NavigateToPrifileStartingToDogListPage();

            bool findElement = userProfileViewObject.IsTextDisplayed(DELETE_DOG_NAME);
            Assert.IsFalse(findElement);
        }
    }
}