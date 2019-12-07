using Moq;
using Prism.Navigation;
using System;
using TP2.Models.Entities;
using TP2.Services;
using TP2.ViewModels;
using TP2.Views;
using Xunit;
using Bogus;
using FluentAssertions;
using System.Collections.Generic;
using System.ComponentModel;
using TP2.UnitTests.Fixtures;

namespace TP2.UnitTests
{
    public class DogDetailViewModelTests : BaseFixture
    {
        private DogDetailViewModel _dogDetailViewModel;
        private Mock<INavigationService> _mockNavigationService;
        private Mock<IShoppingCartService> _mockShoppingCartService;
        private Mock<IAuthenticationService> _mockAuthenticationService;
        private bool _eventRaisedProperty;
        private List<Dog> _dogList;
        private Fixture _fixture = new Fixture();

        public DogDetailViewModelTests()
        {
            _dogList = _fixture.BuildDogsList();
            _mockNavigationService = new Mock<INavigationService>();
            _mockShoppingCartService = new Mock<IShoppingCartService>();
            _mockAuthenticationService = new Mock<IAuthenticationService>();
            //_mockRepositoryService.Setup(r => r.GetAll()).Returns(_dogList);
            _dogDetailViewModel = new DogDetailViewModel(_mockNavigationService.Object, _mockShoppingCartService.Object, _mockAuthenticationService.Object);
        }

        [Fact]
        public void OnNavigatedTo_SelectedDogInformations_ShouldBeDisplayed()
        {
            INavigationParameters navigationParameters = new Prism.Navigation.NavigationParameters();
            Dog dog = _dogList[0];
            navigationParameters.Add("selectedDogData", dog);

            _dogDetailViewModel.OnNavigatedTo(navigationParameters);

            _dogDetailViewModel.SelectedDog.Should().BeEquivalentTo(dog);
            _dogDetailViewModel.SelectedDog.Description.Should().BeEquivalentTo(dog.Description);
            _dogDetailViewModel.SelectedDog.Price.Should().Be(dog.Price);
            _dogDetailViewModel.SelectedDog.Race.Should().BeEquivalentTo(dog.Race);
            _dogDetailViewModel.SelectedDog.Sex.Should().BeEquivalentTo(dog.Sex);
        }

        [Fact]
        public void OnNavigatedTo_ShouldCallContainsFromShoppingCartService()
        {
            INavigationParameters navigationParameters = new Prism.Navigation.NavigationParameters();
            Dog dog = _dogList[0];
            navigationParameters.Add("selectedDogData", dog);

            _dogDetailViewModel.OnNavigatedTo(navigationParameters);

            _mockShoppingCartService.Verify(x => x.Contains(It.IsAny<int>()), Times.Once());
            _mockShoppingCartService.VerifyNoOtherCalls();
        }

        [Fact]
        public void SelectedDog_AddSelectedDogToTheShoppingCart_ShouldAddDogToTheShoppingCartInShoppingCartService()
        {
            _dogDetailViewModel.SelectedDog = _dogList[0];

            _dogDetailViewModel.AddSelectedDogToTheShoppingCart.Execute(null);

            _mockShoppingCartService.Verify(x => x.AddDogToTheShoppingCart(It.IsAny<Dog>()), Times.Once());
        }

        [Fact]
        public void DogAlreadyInTheShoppingCart_AddSelectedDogToTheShoppingCart_ShouldNotBeAccessed()
        {
            _dogDetailViewModel.SelectedDog = _dogList[0];

            _dogDetailViewModel.AddSelectedDogToTheShoppingCart.Execute(null);

            _mockShoppingCartService.Verify(x => x.AddDogToTheShoppingCart(It.IsAny<Dog>()), Times.Once());
        }

        [Fact]
        public void DogAlreadyInTheShoppingCart_AddSelectedDogToTheShoppingCartCommand_ShouldNotBeExecutable()
        {
            _mockShoppingCartService.Setup(r => r.Contains(It.IsAny<int>())).Returns(true);
            _dogDetailViewModel = new DogDetailViewModel(_mockNavigationService.Object, _mockShoppingCartService.Object, _mockAuthenticationService.Object);
            INavigationParameters navigationParameters = new Prism.Navigation.NavigationParameters();
            _dogDetailViewModel.SelectedDog = _dogList[0];

            _dogDetailViewModel.OnNavigatedTo(navigationParameters);

            _dogDetailViewModel.AddSelectedDogToTheShoppingCart.CanExecute(null).Should().BeFalse();
        }

        [Fact]
        public void AddSelectedDogToTheShoppingCartCommand_ShouldNotBeExecutable()
        {
            _mockShoppingCartService.Setup(r => r.Contains(It.IsAny<int>())).Returns(false);

            _dogDetailViewModel.AddSelectedDogToTheShoppingCart.Execute(null);

            _dogDetailViewModel.AddSelectedDogToTheShoppingCart.CanExecute(null).Should().BeFalse();
        }

        [Fact]
        public void SelectedDog_WhenSetToNewValue_ShouldRaisePropertyChangedEvent()
        {
            _dogDetailViewModel.PropertyChanged += RaiseProperty;

            _dogDetailViewModel.SelectedDog = new Faker<Dog>();

            Assert.True(_eventRaisedProperty);
        }

        private void RaiseProperty(object sender, PropertyChangedEventArgs e)
        {
            _eventRaisedProperty = true;
        }
    }
}
