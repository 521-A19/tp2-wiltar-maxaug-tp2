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
                //HandleSelectedDog();
            }
        }

        public DogsListViewModel(INavigationService navigationService,
                                    IRepository<Dog> repositoryService)
            : base(navigationService)
        {
            Title = "Liste des chiens";
            //_dogs = repositoryService.GetAll();
            //var projects = _projects.ToList();
            //return new Collection<Project>(projects);
            Dogs = new ObservableCollection<Dog>
            {
                new Dog()
                {
                    Name = "Rex",
                    ImageUrl = "https://images.dog.ceo/breeds/clumber/n02101556_823.jpg",
                    Price = (float)259.99,
                    Race = "Husky",
                    Sex = "Male",
                    Description = "Jeune chien de 4 mois, super énergique"
                },
                new Dog()
                {
                    Name = "Cloud",
                    ImageUrl = "https://images.dog.ceo/breeds/shiba/shiba-11.jpg",
                    Price = (float)399.99,
                    Race = "Samoyede",
                    Sex = "Male",
                    Description = "13 mois, chien blanc"
                },
                new Dog()
                {
                    Name = "Leo",
                    ImageUrl = "https://images.dog.ceo/breeds/pug/n02110958_1975.jpg",
                    Price = (float)269.99,
                    Race = "Husky",
                    Sex = "Male",
                    Description = "Gentil et calme"
                },
            };
        }

        private void HandleSelectedDog()
        {
            Page page = new Page();
            page.DisplayAlert("Selected item", "Name: " + SelectedDog.Name + " Race: " + SelectedDog.Race, "OK");
        }
    }
}