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
using TP2.UnitTests.Fixture;

namespace TP2.UnitTests
{
    public class ShoppingCartViewModelTests : BaseFixture
    {
        private ShoppingCartViewModel _shoppingCartViewModel;
        private Mock<INavigationService> _mockNavigationService;
        private Mock<IShoppingCartService> _mockShoppingCartService;
        private List<Dog> _dogList;
        private Fixture.Fixture _fixture = new Fixture.Fixture();

        public ShoppingCartViewModelTests()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _mockShoppingCartService = new Mock<IShoppingCartService>();
            _dogList = _fixture.BuildDogsList();
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
    }
}
