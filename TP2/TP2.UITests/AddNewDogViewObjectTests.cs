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

        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp.Android
              .ApkFile(@"C:/Users/usager/source/repos/tp2-wiltar-maxaug-tp2/TP2/TP2/TP2.Android/bin/Release/com.companyname.appname-Signed.apk")
              .StartApp();
        }

        [Test]
        public void JeVeuxMeRendreALaPageDAdoptionDeChien()
        {
            app.Repl();
            const string CONFIRMATION_BUTTON = "Confirmer l'ajout";
            var addNewDogViewObject = new AddNewDogViewObject(app);

            addNewDogViewObject.NavigateToAddNewDogPage();

            AppResult[] results = app.WaitForElement(CONFIRMATION_BUTTON);
            Assert.IsTrue(results.Any());
        }

        [Test]
        public void JeVeuxMeRendreALaPageDAdoptionDeChienEtAjouterUnChien()
        {

            //app.Repl();
            const string EXPECTED_BREED = "affenpinscher";
            var addNewDogViewObject = new AddNewDogViewObject(app);

            addNewDogViewObject.NavigateToAddNewDogPage();
            addNewDogViewObject.AddNewDog();
            app.ScrollDownTo(EXPECTED_BREED);
            

            AppResult[] results = app.WaitForElement(UiText.MAIN_LABEL);
            Assert.IsTrue(results.Any());
        }

    }
}