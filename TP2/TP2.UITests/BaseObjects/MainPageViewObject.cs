using TP2.Externalization;
using Xamarin.UITest;

namespace TP1.UITests.PageObjects
{
    public class MainPageViewObject : BasePageObject
    {
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
