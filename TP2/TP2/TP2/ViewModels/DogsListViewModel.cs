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
                _selectedDog = value;
                HandleSelectedDog();  //Change de page vers DogDetailPage
            }
        }

        private IEnumerable<Dog> _dogs;
        public DogsListViewModel(INavigationService navigationService,
                                    IRepository<Dog> repositoryService)
            : base(navigationService)
        {
            Title = "Liste des chiens";
            var _dogs = repositoryService.GetAll();
            Dogs = new ObservableCollection<Dog>(_dogs);
        }

        private Dog _dog;
        public Dog Dog
        {
            get => _dog;
            set
            {
                _dog = value;
                RaisePropertyChanged();
            }
        }

        private void HandleSelectedDog()
        {
            //Page page = new Page();
            //page.DisplayAlert("Selected item", "Name: " + SelectedDog.Name + " Race: " + SelectedDog.Race, "OK");
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add("registerData", SelectedDog);
            NavigationService.NavigateAsync("NavigationPage/DogDetailPage", navigationParameters);
        }
    }
}