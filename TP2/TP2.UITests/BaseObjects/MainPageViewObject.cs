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
            ClickGoToDogList();
            var dogsListViewObject = new DogsListViewObject(App);
            return dogsListViewObject;
        }

        private void ClickGoToDogList()
        {
            App.Tap(UiText.GO_TO_DOG_LIST);
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

        public DogsListViewObject SignIn()
        {
            EnterLoginAndPasswordAndSignIn();
            var dogsListViewObject = new DogsListViewObject(App);

            return dogsListViewObject;
        }

        private void EnterLoginAndPasswordAndSignIn()
        {
            App.Tap(ID_EMAIL_ENTRY);
            App.EnterText("123");
            App.Tap(ID_PASSWORD_ENTRY);
            App.EnterText(ID_PASSWORD_ENTRY, "456");
            App.Tap(UiText.CONNECTION);
            App.Tap(UiText.CONNECTION);
            App.WaitForElement(UiText.MAIN_LABEL);
        }

        public void FromMasterDetailPageNavigateTo(string urlPage)
        {
            App.TapCoordinates(100, 100);
            /*
            BasePageObject viewObject;
            if (urlPage == UiText.DOG_SHOP_PAGE_MAIN_TITLE)
            {
                viewObject = new DogShopViewObject(App);
            }*/
            App.Tap(urlPage);
            //return viewObject;
        }

        public void ClickSignInButton()
        {
            App.Tap(UiText.CONNECTION);
        }

        public void ClickSignUpButton()
        {
            App.Tap(UiText.SIGN_UP);
        }
        /*
        public MainPageViewObject OpenMainPageView()
        {
            ClickArchivesButton();
            var archivedProjectsViewObject = new ArchivedProjectsViewObject(App);
            return archivedProjectsViewObject;
        }

        public NewProjectViewObject OpenNewProjectViewPage()
        {
            ClickAddNewProjectButton();
            var newProjectViewObject = new NewProjectViewObject(App);
            return newProjectViewObject;
        }

        public ProjectDetailViewObject OpenProjectDetailViewPage(string projectToSelect)
        {
            ClickProjectDetail(projectToSelect);
            var projectDetailViewPage = new ProjectDetailViewObject(App);
            return projectDetailViewPage;
        }


        private void ClickProjectDetail(string projectToSelect)
        {
            App.Tap(projectToSelect);
        }

        private void ClickArchivesButton()
        {
            App.Tap(UiText.ARCHIVES_BUTTON);
        }

        private void ClickAddNewProjectButton()
        {
            App.Tap(UiText.ADD_NEW_PROJECT_BUTTON);
        }*/
    }
}
