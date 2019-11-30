using TP2.UITests.Helpers;
using Xamarin.UITest;

namespace TP2.UITests.BaseObjects
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
