using Prism.Navigation;
using System;
using TP2.Models;
using System.Collections.Generic;
using System.Text;
using TP2.Services;
using Prism.Services;
using Prism.Commands;
using TP2.Models.Entities;
using TP2.Externalization;
using TP2.Views;
using System.Linq;

namespace TP2.ViewModels
{
    public class AddNewDogViewModel : ViewModelBase
    {
        IPageDialogService _dialogService;
        IDogApiService _dogBreedsService;
        IRepository<Dog> _repository;
        IAuthenticationService _authenticationService;
        private RootObject _DogBreeds;
        private List<string> _breedsList;
        private int _selectedBreed = 0;

        private string _name;
        private string _breed;
        private string _sex;
        private string _description;
        private string _imageURl;
        private float _price = 0;

        public DelegateCommand FetchARandomImageCommand => new DelegateCommand(RandomImageURL);
        public DelegateCommand AddNewDogCommand => new DelegateCommand(AddNewDog);

        public AddNewDogViewModel(INavigationService navigationService,
                                    IDogApiService dogBreedsService,
                                    IRepository<Dog> repository,
                                     IPageDialogService dialogService,
                                     IAuthenticationService authenticationService)
            : base(navigationService)
        {
            _dialogService = dialogService;
            _dogBreedsService = dogBreedsService;
            _repository = repository;
            _authenticationService = authenticationService;
            _DogBreeds = _dogBreedsService.GetDogBreeds();
            _breedsList = _DogBreeds.message;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            
             RandomImageURL();

        }

        private void RandomImageURL()
        {
            _breed = _DogBreeds.message[SelectedBreed];
            string urlCall = "https://dog.ceo/api/breed/" + Breed + "/images/random";
            RandomImage image = _dogBreedsService.GetRandomImageURL(urlCall);
            ImageUrl = image.message;
        }

        private async void AddNewDog()
        {
            try {
                
                Dog newDog = new Dog() 
                {
                    Name = Name,
                    Race = Breed,
                    Sex = Sex,
                    Description = Description,
                    ImageUrl = ImageUrl,
                    Price = Price
                };
                _repository.Add(newDog);
                _authenticationService.AuthenticatedUser.DogId = newDog.Id;
               // MakeTheRelationBetweenUserAndDog();

                await NavigationService.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(DogsListPage));
            }
            catch
            {
                await _dialogService.DisplayAlertAsync(UiText.ErrorExceptionThrowTitle, UiText.ErrorExceptionThrowMessage, UiText.Okay);
            }
        }

       /* private void MakeTheRelationBetweenUserAndDog()
        {
            var DogList = _repository.GetAll().ToList();
            var lastDogAddLocation = DogList.Count() - 1;
            var newDogAddId = DogList[lastDogAddLocation];
           
        }*/

        public List<string> DogBreeds
        {
            get => _breedsList;
            set
            {
                _breedsList = value;
                RaisePropertyChanged();
            }
        }
        public int SelectedBreed
        {
            get => _selectedBreed;
            set
            {
                _selectedBreed = value;
                RaisePropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }
        public string Breed
        {
            get => _breed;
            set
            {
                _breed = value;
                RaisePropertyChanged();
            }
        }
        public string Sex
        {
            get => _sex;
            set
            {
                _sex = value;
                RaisePropertyChanged();
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                RaisePropertyChanged();
            }
        }
        public string ImageUrl
        {
            get => _imageURl;
            set
            {
                _imageURl = value;
                RaisePropertyChanged();
            }
        }
        public float Price
        {
            get => _price;
            set
            {
                _price = value;
                RaisePropertyChanged();
            }
        }
    }

}

