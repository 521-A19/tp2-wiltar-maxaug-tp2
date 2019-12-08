using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using TP2.Externalization;
using System.Linq;
using TP2.Models.Entities;
using TP2.Services;
using TP2.Views;
using System.Collections.Generic;

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
                if(_selectedSortType != value)
                {
                    _selectedSortType = value;
                    OrderDogsBy(value);
                    RaisePropertyChanged();
                }
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
            _selectedSortType = -1;
            Title = UiText.DOGS_LIST_PAGE_MAIN_TITLE;
            var _dogs = repositoryService.GetAll();
            Dogs = new ObservableCollection<Dog>(_dogs);
        }

        private void OrderDogsBy(int index)
        {
            IEnumerable<Dog> newDogList = null;
            switch (_selectedSortType)
            {
                case 0:
                    newDogList = Dogs.OrderBy(x => x.Name);
                    break;
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