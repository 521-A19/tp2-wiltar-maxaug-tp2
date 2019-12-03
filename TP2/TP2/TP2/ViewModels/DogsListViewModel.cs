using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TP2.Models.Entities;
using TP2.Services;
using TP2.Views;
using Xamarin.Forms;

namespace TP2.ViewModels
{
    public class DogsListViewModel : ViewModelBase
    {

        private ObservableCollection<Dog> _dogsList;
        private int _selectedSortType;

        public int SelectedSortType
        {
            get => _selectedSortType;
            set
            {
                _selectedSortType = value;
                SortDogListByName();
                RaisePropertyChanged();
            }
        }


        public ObservableCollection<Dog> Dogs
        {
            get => _dogsList;
            set
            {
                _dogsList = value;
                RaisePropertyChanged();
            }
        }

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

        private void SortDogListByName()
        {
            var newDogList = Dogs.OrderBy(x => x.Name);
            switch (_selectedSortType)
            {
                case 1:
                    newDogList = Dogs.OrderBy(x => x.Race);
                    break;

                case 2:
                    newDogList = Dogs.OrderBy(x => x.Price);
                    break;
            }
            Dogs = new ObservableCollection<Dog>(newDogList);
        }

        private void HandleSelectedDog()
        {
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add("selectedDogData", _selectedDog);
            NavigationService.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(DogDetailPage), navigationParameters);
        }
    }
}