using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP1.UITests.PageObjects;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace TP1.UITests
{
    class ProjectsViewObjectTests
    {
        private AndroidApp app;

        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp.Android
              .ApkFile(@"../../../TP2/TP2.Android/bin/Release/com.companyname.TP2-Signed.apk")
              .StartApp();
        }

        [Test]
        public void WelcomeTextIsDisplayed()
        {
            //new MainPageViewObject();
            //Assert.IsTrue(IsTextDisplayed("Welcome to Xamarin Forms and Prism!"));
            //AppResult[] results = app.WaitForElement("Welcome to Xamarin Forms and Prism!");
            //app.Screenshot("Welcome screen.");
            app.WaitForElement("Pet");
            //Assert.IsTrue(results.Any());
        }
    }
}
