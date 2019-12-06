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
    }
}
