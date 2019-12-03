﻿using NUnit.Framework;
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
        private DogsListViewObject _dogListPageViewObject;

        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp.Android
              .ApkFile(@"C:/DevMobile/tp2-wiltar-maxaug-tp2/TP2/TP2/TP2.Android/bin/Release/com.companyname.appname-Signed.apk")
              .StartApp();
            var mainPageViewObject = new MainPageViewObject(app);
            _dogListPageViewObject = new DogsListViewObject(app);
            mainPageViewObject.OpenDogsListPage();
        }

        [Test]
        public void WelcomeTextIsDisplayed()
        {
            AppResult[] results = app.WaitForElement(UiText.MAIN_LABEL);
            //app.Screenshot("Welcome screen.");
            Assert.IsTrue(results.Any());
        }

        [Test]
        public void TrierParRaceChangeLOrdreDesChienEtLeDernierChienEstEnHautDeLaListe()
        {
            //app.Repl();
            _dogListPageViewObject.SelectSortType();
            AppResult[] results = app.WaitForElement("Rex");
            Assert.IsTrue(results.Any());
        }
    }
}
