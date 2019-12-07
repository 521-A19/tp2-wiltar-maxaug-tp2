using Moq;
using Prism.Navigation;
using Prism.Services;
using System;
using TP2.Models.Entities;
using TP2.Services;
using TP2.ViewModels;
using TP2.Views;
using Xunit;
using Bogus;
using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using TP2.UnitTests.Fixtures;

namespace TP2.UnitTests
{
    public class ShoppingCartViewModelTests : BaseFixture
    {
        private ShoppingCartViewModel _shoppingCartViewModel;
        private Mock<INavigationService> _mockNavigationService;
        private Mock<IShoppingCartService> _mockShoppingCartService;
        private Mock<IPageDialogService> _mockPageDialogService;
        private Mock<ISecureStorageService> _mockSecureStorageService;
        private Mock<IAuthenticationService> _mockAuthenticationService;
        private readonly ICryptoService _cryptoService;
        private const string NotEncryptedCreditCard = "5105105105105100";
        private const string EncryptionKey = "--AnEncryptionKey--";
        private List<Dog> _dogList;
        private Fixture _fixture = new Fixture();

        public ShoppingCartViewModelTests()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _mockShoppingCartService = new Mock<IShoppingCartService>();
            _mockPageDialogService = new Mock<IPageDialogService>();
            _mockSecureStorageService = new Mock<ISecureStorageService>();
            _mockAuthenticationService = new Mock<IAuthenticationService>();
            _cryptoService = new CryptoService();
            var user = CreateFakeUser(-1).Generate();
            _mockAuthenticationService.Setup(r => r.AuthenticatedUser).Returns(user);
            _mockSecureStorageService.Setup(r => r.GetUserEncryptionKeyAsync(It.IsAny<User>())).Returns(Task.FromResult(EncryptionKey));
            //_dogList = CreateDogList();
            _dogList = _fixture.BuildDogsList();
            _mockShoppingCartService
                .Setup(r => r.ShoppingCartDogList)
                .Returns(_dogList);
            _shoppingCartViewModel = new ShoppingCartViewModel(_mockNavigationService.Object, _mockShoppingCartService.Object,
                       _mockPageDialogService.Object, _mockAuthenticationService.Object, _mockSecureStorageService.Object, _cryptoService);
        }

        [Fact]
        public void DogList_OnArrival_DogListObservableShouldBeLoaded()
        {
            foreach (Dog cur in _dogList) { _shoppingCartViewModel.DogList.Should().Contain(cur); }
        }

        [Fact]
        public void DeleteDogFromTheShoppingCartCommand_ShouldCallRemoveTheDogFromTheShoppingCartAndNavigateToShoppingCartPage()
        {
            _shoppingCartViewModel.DeleteDogFromTheShoppingCartCommand.Execute(_dogList[0]);

            _mockShoppingCartService.Verify(x => x.RemoveDogFromTheShoppingCart(It.IsAny<Dog>()), Times.Once());
            _mockNavigationService.Verify(x => x.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(ShoppingCartPage)), Times.Once());
        }

        [Fact]
        public void BuyShoppingCartCommand_ShouldNavigateToConfirmationPage()
        {
            _shoppingCartViewModel.BuyShoppingCartCommand.Execute(null);

            _mockNavigationService.Verify(x => x.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(DogsListPage)), Times.Once());
        }

        [Fact]
        public void BuyShoppingCartCommand_ShouldCallBuyShoppingCart()
        {
            _shoppingCartViewModel.BuyShoppingCartCommand.Execute(null);

            _mockShoppingCartService.Verify(x => x.BuyShoppingCart(), Times.Once());
        }
   
        [Fact]
        public void CancelShoppingCartCommand_ShouldNavigateToConfirmationPage()
        {
            _shoppingCartViewModel.CancelShoppingCartCommand.Execute(null);

            _mockNavigationService.Verify(x => x.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(DogsListPage)), Times.Once());
        }

        [Fact]
        public void CancelShoppingCartCommand_ShouldCallSetNewEmptyShoppingCart()
        {
            _shoppingCartViewModel.CancelShoppingCartCommand.Execute(null);

            _mockShoppingCartService.Verify(x => x.SetNewEmptyShoppingCart(), Times.Once());
        }

        
        [Fact]
        public void OnNavigatedTo_ShouldCallGetUserEncryptionKey()
        {
            INavigationParameters navigationParameters = new Prism.Navigation.NavigationParameters();

            _shoppingCartViewModel.OnNavigatedTo(navigationParameters);

            _mockSecureStorageService.Verify(x => x.GetUserEncryptionKeyAsync(It.IsAny<User>()), Times.Once());
        }

        [Fact]
        public void NotEmptyDogList_BuyShoppingCartCommand_CanExecuteShoulReturnTrue()
        {
            _shoppingCartViewModel.DogList = new ObservableCollection<Dog>(_dogList);

            _shoppingCartViewModel.BuyShoppingCartCommand.CanExecute(null).Should().BeTrue();
        }

        private Faker<User> CreateFakeUser(int dogId)
        {
            var fakeUser = new Faker<User>()
                .StrictMode(true)
                .RuleFor(u => u.Id, f => f.IndexFaker)
                .RuleFor(u => u.Login, f => f.Person.Email)
                .RuleFor(u => u.CreditCard, f => _cryptoService.Encrypt(NotEncryptedCreditCard, EncryptionKey))
                .RuleFor(u => u.HashedPassword, f => "264531")
                .RuleFor(u => u.PasswordSalt, f => "adskadk")
                .RuleFor(u => u.DogId, f => dogId);
            return fakeUser;
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
