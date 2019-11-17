using Prism.Commands;
using Prism.Navigation;
using System;
using TP2.Models.Entities;

namespace TP2.ViewModels
{
    public class DogShopViewModel : ViewModelBase
    {
        public DelegateCommand GoToCommand => new DelegateCommand(ChangePage);
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

        public DogShopViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Mon pet shop";
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

        private void ChangePage()
        {
            NavigationService.NavigateAsync(new Uri("DogsListPage/DogDetailPage", UriKind.Relative));
        }
    }
}