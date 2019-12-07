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
    public class RegisterViewObjectTests
    {
        private AndroidApp app;
        private MainPageViewObject _mainPageViewObject;
        private RegisterViewObject _registerViewObject;

        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp.Android
              .ApkFile(@"C:/DevMobile/tp2-wiltar-maxaug-tp2/TP2/TP2/TP2.Android/bin/Release/com.companyname.appname-Signed.apk")
              .StartApp();
            _mainPageViewObject = new MainPageViewObject(app);
            _registerViewObject = _mainPageViewObject.ClickSignUpButton();
        }

        [Test]
        public void OnRegisterPage_TitleIsDisplayed()
        {
            Assert.IsTrue(_registerViewObject.IsTextDisplayed(UiText.REGISTER_PAGE_MAIN_TITLE));
        }

        [Test]
        public void InvalidEmail_ShouldDisplayErrorMessage()
        {
            const string INVALID_EMAIL = "test@";

            _registerViewObject.EnterEmail(INVALID_EMAIL);

            Assert.IsTrue(_registerViewObject.IsTextDisplayed(UiText.ValidEmailRequired));
        }


        [Test]
        public void InvalidPasswordWithoutANumericCharacter_ShouldDisplayErrorMessage()
        {
            const string INVALID_PASSWORD = "testA";

            _registerViewObject.EnterPassword(INVALID_PASSWORD);

            Assert.IsTrue(_registerViewObject.IsTextDisplayed(UiText.NumericCharacterRequired));
        }

        [Test]
        public void InvalidPasswordWithoutAUppercaseLetter_ShouldDisplayErrorMessage()
        {
            const string INVALID_PASSWORD = "test1";

            _registerViewObject.EnterPassword(INVALID_PASSWORD);

            Assert.IsTrue(_registerViewObject.IsTextDisplayed(UiText.UppercaseRequired));
        }

        [Test]
        public void InvalidPasswordWithoutALowercaseLetter_ShouldDisplayErrorMessage()
        {
            const string INVALID_PASSWORD = "TEST1";

            _registerViewObject.EnterPassword(INVALID_PASSWORD);

            Assert.IsTrue(_registerViewObject.IsTextDisplayed(UiText.LowercaseRequired));
        }

        [Test]
        public void InvalidPasswordWithoutTenCharacters_ShouldDisplayErrorMessage()
        {
            const string INVALID_PASSWORD = "testA1";

            _registerViewObject.EnterPassword(INVALID_PASSWORD);

            Assert.IsTrue(_registerViewObject.IsTextDisplayed(UiText.MoreThanTenCharactersRequired));
        }


        
        [Test]
        public void JeVeuxMeRendreALaPageDEnregistrationEtMenregistrerMaisDesMessagesDerreursApparaient()
        {
            const string VALID_PASSWORD = "testingA123";
            _registerViewObject.EnterPassword(VALID_PASSWORD);
            const string INVALID_CONFIRM_PASSWORD = "test";

            _registerViewObject.EnterConfirmationPassword(INVALID_CONFIRM_PASSWORD);
            _registerViewObject.TapButton(UiText.BUTTON_CONFIRM_REGISTRATION);

            Assert.IsTrue(_registerViewObject.IsTextDisplayed(UiText.SecondPasswordIsTheSameOfTheFirst));
        }

        [Test]
        public void EverythingValid_ButtonRegister_ShouldNavigateToMainPage()
        {
            const string VALID_EMAIL = "test@email.com";
            _registerViewObject.EnterEmail(VALID_EMAIL);
            const string VALID_PASSWORD = "testingA123";
            _registerViewObject.EnterPassword(VALID_PASSWORD);
            const string VALID_CONFIRM_PASSWORD = "testingA123";
            _registerViewObject.EnterConfirmationPassword(VALID_CONFIRM_PASSWORD);

            _mainPageViewObject = _registerViewObject.TapRegister();

            Assert.IsTrue(_registerViewObject.IsTextDisplayed(UiText.WELCOME_ON_DOGFINDER));
        }
    }
}