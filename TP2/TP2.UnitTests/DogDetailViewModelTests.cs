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
        private bool _eventRaisedProperty;

        public DogDetailViewModelTests()
        {
            _mockNavigationService = new Mock<INavigationService>();
            //_mockRepositoryService.Setup(r => r.GetAll()).Returns(_dogList);
            _dogDetailViewModel = new DogDetailViewModel(_mockNavigationService.Object);
        }

        [Fact]
        public void OnArrival_DogsObservableListShouldBeLoaded()
        {
            //var fakeDog = CreateFakeDog();
            var dog = new Dog()
            {
                Name = "Leo",
                ImageUrl = "https://images.dog.ceo/breeds/pug/n02110958_1975.jpg",
                Price = (float)269.99,
                Race = "Husky",
                Sex = "Male",
                Description = "Gentil et calme"

            };
            var navigationParameters = new NavigationParameters();
            //_mockNavigationParameters.Setup(r => r.Add("data", user));
            navigationParameters.Add("selectedDogData", dog);
            //_mockSecureStorageService.Setup(a => a.GetUserEncryptionKeyAsync(user)).ReturnsAsync(key);
            _dogDetailViewModel.OnNavigatedTo(navigationParameters);
            _dogDetailViewModel.SelectedDog.Should().BeEquivalentTo(dog);
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
