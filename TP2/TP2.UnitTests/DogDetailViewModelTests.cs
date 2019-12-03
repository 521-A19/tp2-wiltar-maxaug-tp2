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

namespace TP2.UnitTests
{
    public class DogDetailViewModelTests
    {
        private DogDetailViewModel _dogDetailViewModel;
        private Mock<INavigationService> _mockNavigationService;
        private Mock<IShoppingCartService> _mockShoppingCartService;
        private bool _eventRaisedProperty;

        public DogDetailViewModelTests()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _mockShoppingCartService = new Mock<IShoppingCartService>();
            //_mockRepositoryService.Setup(r => r.GetAll()).Returns(_dogList);
            _dogDetailViewModel = new DogDetailViewModel(_mockNavigationService.Object, _mockShoppingCartService.Object);
        }

        [Fact]
        public void OnNavigatedTo_SelectedDogInformations_ShouldBeDisplayed()
        {
            INavigationParameters navigationParameters = new Prism.Navigation.NavigationParameters();
            Dog dog = CreateFakeDog();
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
            Dog dog = CreateFakeDog();
            navigationParameters.Add("selectedDogData", dog);

            _dogDetailViewModel.OnNavigatedTo(navigationParameters);

            _mockShoppingCartService.Verify(x => x.Contains(It.IsAny<int>()), Times.Once());
            _mockShoppingCartService.VerifyNoOtherCalls();
        }

        [Fact]
        public void SelectedDog_AddSelectedDogToTheShoppingCart_ShouldAddDogToTheShoppingCartInShoppingCartService()
        {
            _dogDetailViewModel.SelectedDog = CreateFakeDog();

            _dogDetailViewModel.AddSelectedDogToTheShoppingCart.Execute(null);

            _mockShoppingCartService.Verify(x => x.AddDogToTheShoppingCart(It.IsAny<Dog>()), Times.Once());
        }

        [Fact]
        public void DogAlreadyInTheShoppingCart_AddSelectedDogToTheShoppingCart_ShouldNotBeAccessed()
        {
            _dogDetailViewModel.SelectedDog = CreateFakeDog();

            _dogDetailViewModel.AddSelectedDogToTheShoppingCart.Execute(null);

            _mockShoppingCartService.Verify(x => x.AddDogToTheShoppingCart(It.IsAny<Dog>()), Times.Once());
        }

        [Fact]
        public void DogAlreadyInTheShoppingCart_AddSelectedDogToTheShoppingCartCommand_ShouldNotBeExecutable()
        {
            _mockShoppingCartService.Setup(r => r.Contains(It.IsAny<int>())).Returns(true);
            _dogDetailViewModel = new DogDetailViewModel(_mockNavigationService.Object, _mockShoppingCartService.Object);
            INavigationParameters navigationParameters = new Prism.Navigation.NavigationParameters();
            _dogDetailViewModel.SelectedDog = CreateFakeDog();

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

        private Faker<Dog> CreateFakeDog()
        {
            var fakeDog = new Faker<Dog>()
                .StrictMode(true)
                .RuleFor(u => u.Name, f => f.Person.FirstName)
                .RuleFor(u => u.Price, f => (float)299.99)
                .RuleFor(u => u.Race, f => "Husky")
                .RuleFor(u => u.Description, f => "Dog")
                .RuleFor(u => u.Sex, f => f.Person.Gender.ToString())
                .RuleFor(u => u.ImageUrl, f => "url")
                .RuleFor(u => u.Id, f => f.IndexFaker);
            return fakeDog;
        }
    }
}
