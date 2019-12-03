using Bogus;
using FluentAssertions;
using Moq;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using TP2.Models.Entities;
using TP2.Services;
using TP2.ViewModels;
using TP2.Views;
using Xunit;

namespace TP2.UnitTests
{
    public class UserProfileViewModelTests
    {
        private List<User> _userList;
        private List<Dog> _dogList;
        UserProfileViewModel _userProfileViewModel;
        private Mock<INavigationService> _mockNavigationService;
        private Mock<IRepository<Dog>> _mockRepository;
        private Mock<IAuthenticationService> _mockAuthentification;
        private const string NotHashedPassword = "Qwertyuiop1";
        private const string NotEncryptedCreditCard = "5105105105105100";
        private const string EncryptionKey = "--AnEncryptionKey--";
        public UserProfileViewModelTests()
        {
            _userList = CreateUserList();
            _dogList = CreateDogList();
            _mockNavigationService = new Mock<INavigationService>();
            _mockRepository = new Mock<IRepository<Dog>>();
            _mockAuthentification = new Mock<IAuthenticationService>();
            _mockAuthentification.Setup(n => n.AuthenticatedUser).Returns(_userList[0]);
            _userProfileViewModel = new UserProfileViewModel(_mockNavigationService.Object, _mockAuthentification.Object, _mockRepository.Object);
        }

        [Fact]
        public void DeleteDogShopCommand_WhenUserHaveADogAndDeleteDog_ShouldDogIdEqualsNegatifOne()
        {
            const int EXPECTED_USER_DOG_ID = -1;
            _userProfileViewModel.MyDog = _dogList[0];

            _userProfileViewModel.DeleteDogShopCommand.Execute();

            _userProfileViewModel.UserLogIn.DogId.Should().Equals(EXPECTED_USER_DOG_ID);
        }

        [Fact]
        public void DeleteDogShopCommand_WhenUserHaveADogAndDeleteDog_ShouldNavigateToDogListPage()
        {
            _userProfileViewModel.MyDog = _dogList[0];

            _userProfileViewModel.DeleteDogShopCommand.Execute();

            _mockNavigationService.Verify(x => x.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(DogsListPage)), Times.Once());
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
