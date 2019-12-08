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
using TP2.Validation;
using TP2.Validation.Rules;

namespace TP2.ViewModels
{
    public class AddNewDogViewModel : ViewModelBase
    {
        IPageDialogService _dialogService;
        IDogApiService _dogBreedsService;
        IRepository<Dog> _dogRepository;
        IRepository<User> _userRepository;
        IAuthenticationService _authenticationService;
        private RootObject _DogBreeds;
        private List<string> _breedsList;
        private int _selectedBreed = 0;

        private ValidatableObject<string> _name;
        private string _breed;
        private string _sex;
        private string _description;
        private string _imageURl;
        private ValidatableObject<float> _price;

        public DelegateCommand FetchARandomImageCommand => new DelegateCommand(RandomImageURL);
        public DelegateCommand AddNewDogCommand => new DelegateCommand(AddNewDog);
        public DelegateCommand ValidateDogNameCommand => new DelegateCommand(ValidateDogName);
        public DelegateCommand ValidateDogPriceCommand => new DelegateCommand(ValidateDogPrice);

        public AddNewDogViewModel(INavigationService navigationService,
                                    IDogApiService dogBreedsService,
                                    IRepository<Dog> dogRepository,
                                     IRepository<User> userRepository,
                                     IPageDialogService dialogService,
                                     IAuthenticationService authenticationService)
            : base(navigationService)
        {
            Title = UiText.ADD_NEW_DOG_PAGE_MAIN_TITLE;
            _dialogService = dialogService;
            _dogBreedsService = dogBreedsService;
            _DogBreeds = _dogBreedsService.GetDogBreeds();
            _breedsList = _DogBreeds.message;
            _dogRepository = dogRepository;
            _userRepository = userRepository;
            _authenticationService = authenticationService;
            _name = new ValidatableObject<string>();
            _price = new ValidatableObject<float>();
            AddValidationRulesToValidatable();
        }
        private void ValidateDogName()
        {
            _name.Validate();
        }

        private void ValidateDogPrice()
        {
            _price.Validate();
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
                ValidateDogName();
                ValidateDogPrice();
                if (EntriesHaveNoError())
                {
                    Dog newDog = new Dog() 
                {
                    Name = Name.Value,
                    Race = Breed,
                    Sex = Sex,
                    Description = Description,
                    ImageUrl = ImageUrl,
                    Price = Price.Value
                };
                _dogRepository.Add(newDog);  // Le Add du repo incrémente les nouveaux chiens
                _authenticationService.AuthenticatedUser.DogId = newDog.Id;
                _userRepository.Update(_authenticationService.AuthenticatedUser);
                await NavigationService.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(DogsListPage));
            }
            }
            catch
            {
                await _dialogService.DisplayAlertAsync(UiText.ErrorExceptionThrowTitle, UiText.ErrorExceptionThrowMessage, UiText.Okay);
            }
        }

        private bool EntriesHaveNoError()
        {
            if (Name.Errors.Count + Price.Errors.Count == 0) return true;
            return false;
        }

        private void AddValidationRulesToValidatable()
        {
            var isBiggerThanZero = new IsBiggerThanZero<float>
            {
                ErrorMessage = UiText.DOG_NEED_A_GOOD_PRICE
            };
            var haveAName = new HaveAName<string>
            {
                ErrorMessage = UiText.DOG_NEED_A_NAME
            };
            _name.AddValidationRule(haveAName);
            _price.AddValidationRule(isBiggerThanZero);
        }

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

        public ValidatableObject<string> Name
        {
            get => _name;

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
        public ValidatableObject<float> Price
        {
            get => _price;
        }
    }

}

