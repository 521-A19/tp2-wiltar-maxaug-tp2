using TP1.UITests.Helpers;
using Xamarin.UITest;

namespace TP1.UITests.PageObjects
{
    public class BasePageObject
    {
        protected IApp App;
        public bool IsDisplayed { get; }

        public BasePageObject(IApp app)
        {
            App = app;
        }

        public bool IsTextDisplayed(string textToFind)
        {
            return UiTestHelpers.IsTextDisplayed(App, textToFind);
        }
    }
}
