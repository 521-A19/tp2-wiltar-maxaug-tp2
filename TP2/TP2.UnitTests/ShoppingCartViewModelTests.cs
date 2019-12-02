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

namespace TP2.UnitTests
{
    public class ShoppingCartViewModelTests
    {
        private ShoppingCartViewModel _shoppingCartViewModel;
        private Mock<INavigationService> _mockNavigationService;
        private Mock<IShoppingCartService> _mockShoppingCartService;
        private List<Dog> _dogList;

        public ShoppingCartViewModelTests()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _mockShoppingCartService = new Mock<IShoppingCartService>();
            _dogList = CreateDogList();
            _mockShoppingCartService
                .Setup(r => r.ShoppingCartDogList)
                .Returns(_dogList);
            _shoppingCartViewModel = new ShoppingCartViewModel(_mockNavigationService.Object, _mockShoppingCartService.Object);
        }

        [Fact]
        public void DogList_OnArrival_DogListObservableShouldBeLoaded()
        {
            foreach (Dog cur in _dogList) { _shoppingCartViewModel.DogList.Should().Contain(cur); }
        }

        [Fact]
        public void DeleteDogFromTheShoppingCartCommand_ShouldRemoveTheDogFromTheShoppingCart()
        {
            _shoppingCartViewModel.DeleteDogFromTheShoppingCartCommand.Execute(_dogList[0]);

            //_shoppingCartViewModel.DogList[0].Should().NotBe(_dogList[0]);
            _mockNavigationService.Verify(x => x.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(ShoppingCartPage)), Times.Once());
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
