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
                RaisePropertyChanged();
            }
        }

        public DogDetailViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            
        }

        public override void OnNavigatedTo(INavigationParameters parameters) //Est appelé avant l'affichage de la page
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("selectedDogData"))
            {
                SelectedDog = parameters["selectedDogData"] as Dog;
                Title = "Voici le chien " + SelectedDog.Name;
            }

            //get a single parameter as type object, which must be cast
            //var color = parameters["color"] as Color;

            //get a single typed parameter
            //var color = parameters.GetValue<Color>("color");

            //get a collection of typed parameters
            //var colors = parameters.GetValues<Color>("colors");
        }

    }
}