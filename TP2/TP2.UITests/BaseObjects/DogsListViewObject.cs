using Xamarin.UITest;

namespace TP2.UITests.BaseObjects
{
    public class DogsListViewObject : BasePageObject
    {
        public DogsListViewObject(IApp app) : base(app)
        {
        }

        public void SelectSortType()
        {
            App.Tap("Nom");
            App.Tap("Race");
        }
    }
}
