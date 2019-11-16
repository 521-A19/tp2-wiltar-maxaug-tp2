using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TP2.Models.Entities;
using TP2.Services;
using Xamarin.Forms;

namespace TP2.ViewModels
{
    public class DogsListViewModel : ViewModelBase
    {
        public ObservableCollection<Dog> Dogs { get; set; }

        private Dog _selectedDog;
        public Dog SelectedDog
        {
            get { return _selectedDog; }
            set 
            { 
                if (_selectedDog != value)
                {
                    _selectedDog = value;
                    HandleSelectedDog();  //Change de page vers DogDetailPage
                }
            }
        }

        public DogsListViewModel(INavigationService navigationService,
                                    IRepository<Dog> repositoryService)
            : base(navigationService)
        {
            Title = "Liste des chiens";
            var _dogs = repositoryService.GetAll();
            Dogs = new ObservableCollection<Dog>(_dogs);
        }

        private void HandleSelectedDog()
        {
            //Page page = new Page();
            //page.DisplayAlert("Selected item", "Name: " + SelectedDog.Name + " Race: " + SelectedDog.Race, "OK");
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add("selectedDogData", _selectedDog);
            NavigationService.NavigateAsync("DogDetailPage", navigationParameters);
        }
    }
}