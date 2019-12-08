using TP2.Externalization;
using Xamarin.UITest;

namespace TP2.UITests.BaseObjects
{
    public class DogsListViewObject : BasePageObject
    {
        public DogsListViewObject(IApp app) : base(app)
        {
        }

        public DogDetailViewObject OpenDogDetailViewPage(string dogToSelect)
        {
            ClickDogDetail(dogToSelect);
            var dogDetailViewPage = new DogDetailViewObject(App);
            return dogDetailViewPage;
        }

        private void ClickDogDetail(string dogToSelect)
        {
            App.Tap(dogToSelect);
        }

        public void SelectSortType(string sort)
        {
            App.Tap("Trier par");
            App.Tap(sort);
        }

        public void Search(string dog)
        {
            App.WaitForElement(UiText.DOGS_LIST_PAGE_MAIN_LABEL);
            App.TapCoordinates(0, 0);
            App.ScrollDownTo(dog);
        }
    }
}
