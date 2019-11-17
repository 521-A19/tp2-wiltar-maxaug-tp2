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
using Prism.Services;

namespace TP2.UnitTests
{
    public class DogShopViewModelTests
    {
        private DogShopViewModel _dogShopViewModel;
        private Mock<INavigationService> _mockNavigationService;
        private Mock<IAuthenticationService> _mockAuthenticationService;
        private Mock<IPageDialogService> _mockPageDialogService;
        private bool _eventRaisedProperty;

        public DogShopViewModelTests()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _mockAuthenticationService = new Mock<IAuthenticationService>();
            _mockPageDialogService = new Mock<IPageDialogService>();
            //_mockRepositoryService.Setup(r => r.GetAll()).Returns(_dogList);
            _dogShopViewModel = new DogShopViewModel(_mockNavigationService.Object, _mockAuthenticationService.Object, _mockPageDialogService.Object);
        }

        [Fact]
        public void UserIsNotConnected_OnNavigatedPage_ShouldDisplayAlertMessage()
        {
            //_mockUserRepository.Setup(r => r.IsExisting(It.IsAny<string>())).Returns(false);
            _mockPageDialogService.Verify(x => x.DisplayAlertAsync("Attention", "Vous devez être connecté pour placer en adoption votre chien", "D'accord"));
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
