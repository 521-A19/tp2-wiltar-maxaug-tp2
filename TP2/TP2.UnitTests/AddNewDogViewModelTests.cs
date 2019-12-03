using Bogus;
using FluentAssertions;
using Moq;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using TP2.Externalization;
using TP2.Models.Entities;
using TP2.Services;
using TP2.ViewModels;
using TP2.Views;
using Xunit;

namespace TP2.UnitTests
{
    public class AddNewDogViewModelTests
    {
        AddNewDogViewModel _addNewDogViewModel;
        private const string NotHashedPassword = "Qwertyuiop1";
        private const string NotEncryptedCreditCard = "5105105105105100";
        private const string EncryptionKey = "--AnEncryptionKey--";
        private Mock<INavigationService> _mockNavigationService;
        private Mock<IPageDialogService> _mockPageDialogService;
        private Mock<IRepository<Dog>> _mockRepository;
        private Mock<IAuthenticationService> _mockAuthentification;
        private IDogApiService _dogApiService;
        private List<User> _userList;
        private List<Dog> _dogList;

        public AddNewDogViewModelTests()
        {
            _userList = CreateUserList();
            _dogList = CreateDogList();
            _mockNavigationService = new Mock<INavigationService>();
            _mockPageDialogService = new Mock<IPageDialogService>();
            _mockRepository = new Mock<IRepository<Dog>>();
            _dogApiService = new DogApiService();
            _mockAuthentification = new Mock<IAuthenticationService>();
            _addNewDogViewModel = new AddNewDogViewModel(_mockNavigationService.Object, _dogApiService, _mockRepository.Object, _mockPageDialogService.Object, _mockAuthentification.Object);
        }

        [Fact]
        public void AddNewDogCommand_WhenAllDogAttributIsHonored_ShouldNavigateToDogsList()
        {
            _mockAuthentification
              .Setup(a => a.AuthenticatedUser)
              .Returns(_userList[0]);

            _mockRepository
                .Setup(n => n.GetAll())
                .Returns(_dogList);

            _addNewDogViewModel.Name = "Dog";
            _addNewDogViewModel.Breed = "african";
            _addNewDogViewModel.Description = "Description";
            _addNewDogViewModel.Sex = "Male";
            _addNewDogViewModel.Price = 120;
            _addNewDogViewModel.FetchARandomImageCommand.Execute();

            _addNewDogViewModel.AddNewDogCommand.Execute();

            _mockNavigationService.Verify(x => x.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(DogsListPage)), Times.Once());
        }

        [Fact]
        public void AddNewDogCommand_WhenAllDogAttributIsNotReallyHonored_ShouldStillNavigateToDogsList()
        {
            _mockAuthentification
              .Setup(a => a.AuthenticatedUser)
              .Returns(_userList[0]); ;

            _mockRepository
               .Setup(n => n.GetAll())
               .Returns(_dogList);

            _addNewDogViewModel.Name = "Dog";
            _addNewDogViewModel.Breed = "african";
            _addNewDogViewModel.Price = 120;
            _addNewDogViewModel.FetchARandomImageCommand.Execute();

            _addNewDogViewModel.AddNewDogCommand.Execute();

            _mockNavigationService.Verify(x => x.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(DogsListPage)), Times.Once());
        }

        [Fact]
        public void AddNewDogCommand_WhenExectionIsThrow_ShouldSetAlert()
        {
            //Arrange
            _mockAuthentification
               .Setup(a => a.AuthenticatedUser)
               .Returns(_userList[0]); ;

            _addNewDogViewModel.Breed = "african";
            _addNewDogViewModel.Price = 120;

            _mockNavigationService
                .Setup(a => a.NavigateAsync("AddNewDogPage/DogsListPage"))
                .Throws<Exception>();

            //Act
            _addNewDogViewModel.AddNewDogCommand.Execute();
            //Assert
            _mockPageDialogService.Verify(x => x.DisplayAlertAsync(UiText.ErrorExceptionThrowTitle, UiText.ErrorExceptionThrowMessage, UiText.Okay));
        }

        

        [Fact]
        public void FetchARandomImageCommand_WhenAllDogAttributIsNotReallyHonored_ShouldStillNavigateToDogsList()
        {
            _mockAuthentification
              .Setup(a => a.AuthenticatedUser)
              .Returns(_userList[0]); ;

            _addNewDogViewModel.Breed = "african";

            _addNewDogViewModel.FetchARandomImageCommand.Execute();

            _addNewDogViewModel.ImageUrl.Should().NotBeNull();        
        }

        [Fact]
        public void FetchARandomImageCommand_WhenAllDogAttributNotReallyHonored_ShouldStillNavigateToDogsList()
        {
            _mockAuthentification
              .Setup(a => a.AuthenticatedUser)
              .Returns(_userList[0]); ;

            _addNewDogViewModel.Breed = "african";

            _addNewDogViewModel.FetchARandomImageCommand.Execute();
            string FIRST_IMAGE_RETURN = _addNewDogViewModel.ImageUrl;

            _addNewDogViewModel.FetchARandomImageCommand.Execute();
            string SECOND_IMAGE_RETURN = _addNewDogViewModel.ImageUrl;

            FIRST_IMAGE_RETURN.Should().NotContainEquivalentOf(SECOND_IMAGE_RETURN);
        }

        private List<User> CreateUserList()
        {
            var crypto = new CryptoService();
            var salt = crypto.GenerateSalt();
            var userList = new Faker<User>()
                .StrictMode(true)
                .RuleFor(u => u.Login, f => f.Person.Email)
                .RuleFor(u => u.PasswordSalt, f => salt)
                .RuleFor(u => u.CreditCard, f => crypto.Encrypt(NotEncryptedCreditCard, EncryptionKey))
                .RuleFor(u => u.HashedPassword, f => crypto.HashSHA512(NotHashedPassword, salt))
                .RuleFor(u => u.Id, f => f.IndexFaker)
                .RuleFor(u => u.DogId, f => 1)
                .Generate(3);
            return userList;
        }

        private List<Dog> CreateDogList()
        {
            var dogList = new Faker<Dog>()
                .StrictMode(true)
                .RuleFor(u => u.Name, f => f.Person.FirstName)
                .RuleFor(u => u.Price, f => (float)299.99)
                .RuleFor(u => u.Race, f => "Husky")
                .RuleFor(u => u.Description, f => "Dog")
                .RuleFor(u => u.Sex, f => f.Person.Gender.ToString())
                .RuleFor(u => u.ImageUrl, f => "url")
                .RuleFor(u => u.Id, f => f.IndexFaker)
                .Generate(3);
            return dogList;
        }

    }
}
