using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TP2.Models.Entities;
using TP2.Services;
using TP2.Views;
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
                HandleSelectedDog();
            }
        }

        public DogsListViewModel(INavigationService navigationService,
                                    IRepository<Dog> repositoryService)
            : base(navigationService)
        {
            Title = "Liste de chiens globale";
            var _dogs = repositoryService.GetAll();
            Dogs = new ObservableCollection<Dog>(_dogs);
        }

        private void HandleSelectedDog()
        {
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add("selectedDogData", _selectedDog);
            NavigationService.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(DogDetailPage), navigationParameters);
        }
    }
}