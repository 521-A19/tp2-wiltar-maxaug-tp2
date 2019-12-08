using TP2.Externalization;
using Xamarin.UITest;

namespace TP2.UITests.BaseObjects
{
    public class MainPageViewObject : BasePageObject
    {
        const string ID_EMAIL_ENTRY = "NameId";
        const string ID_PASSWORD_ENTRY = "PasswordId";

        public MainPageViewObject(IApp app) : base(app)
        {
        }

        public DogsListViewObject OpenDogsListPage()
        {
            TapButton(UiText.GO_TO_DOG_LIST);
            return new DogsListViewObject(App);
        }

        public void EnterLogin(string login)
        {
            App.WaitForElement(UiText.EMAIL);
            App.EnterText(UiText.EMAIL, login);
            App.DismissKeyboard();
        }
        public void EnterPassword(string password)
        {
            App.Tap(UiText.PASSWORD);
            App.EnterText(UiText.PASSWORD, password);
            App.DismissKeyboard();
        }

        public DogsListViewObject UserHasDogSignIn()
        {
            EnterLoginAndPasswordAndSignIn("123","456");
            return new DogsListViewObject(App); ;
        }

        public DogsListViewObject UserHasNoDogSignIn()
        {
            EnterLoginAndPasswordAndSignIn("456","789");
            return new DogsListViewObject(App); ;
        }

        private void EnterLoginAndPasswordAndSignIn(string email, string password)
        {
            App.Tap(ID_EMAIL_ENTRY);
            App.EnterText(email);
            App.Tap(ID_PASSWORD_ENTRY);
            App.EnterText(ID_PASSWORD_ENTRY, password);
            App.DismissKeyboard();
            App.Tap(UiText.CONNECTION); // ou x2
            App.WaitForElement(UiText.DOGS_LIST_PAGE_MAIN_LABEL);
        }

        public RegisterViewObject ClickSignUpButton()
        {
            App.Tap(UiText.SIGN_UP);
            return new RegisterViewObject(App);
        }
    }
}
