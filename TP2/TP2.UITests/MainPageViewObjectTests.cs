using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP1.UITests.PageObjects;
using TP2.Externalization;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace TP2.UITests
{
    class ProjectsViewObjectTests
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
        public void WelcomeTextIsDisplayed()
        {
            AppResult[] results = app.WaitForElement(UiText.WELCOME_ON_DOGFINDER);
            //app.Screenshot("Welcome screen.");
            Assert.IsTrue(results.Any());
        }
    }
}
