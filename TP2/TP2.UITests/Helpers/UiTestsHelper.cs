using Xamarin.UITest;

namespace TP1.UITests.Helpers
{
    public static class UiTestHelpers
    {
        public static bool IsTextDisplayed(IApp app, string text)
        {
            try
            {
                app.WaitForElement(text);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
