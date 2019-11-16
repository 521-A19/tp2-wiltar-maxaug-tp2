using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TP2.Models.Entities;
using TP2.Services;
using Xamarin.Forms;

namespace TP2.ViewModels
{
    public class DogDetailViewModel : ViewModelBase
    {
        private Dog _selectedDog;
        public Dog SelectedDog
        {
            get { return _selectedDog; }
            set
            {
                _selectedDog = value;
                HandleSelectedDog();  //Change de page vers DogDetailPage
            }
        }

        public DogDetailViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Voici le chien...";
        }

        private void HandleSelectedDog()
        {
            //Page page = new Page();
            //page.DisplayAlert("Selected item", "Name: " + SelectedDog.Name + " Race: " + SelectedDog.Race, "OK");
        }
    }
}