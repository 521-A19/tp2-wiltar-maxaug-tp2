using Moq;
using Prism.Navigation;
using System;
using TP2.Models.Entities;
using TP2.Views;
using Xunit;
using Bogus;
using FluentAssertions;
using System.Collections.Generic;
using System.ComponentModel;
using TP2.ViewModels.MasterDetailViews;
using TP2.Services;
using System.Threading.Tasks;

namespace TP2.UnitTests
{
    public class CustomMasterDetailViewModelTests
    {
        private CustomMasterDetailViewModel _customMasterDetailViewModel;
        private Mock<INavigationService> _mockNavigationService;
        private Mock<IAuthenticationService> _mockAuthenticationService;
        private Mock<IShoppingCartService> _mockShoppingCartService;

        public CustomMasterDetailViewModelTests()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _mockAuthenticationService = new Mock<IAuthenticationService>();
            _mockShoppingCartService = new Mock<IShoppingCartService>();
            _customMasterDetailViewModel = new CustomMasterDetailViewModel(_mockNavigationService.Object, _mockAuthenticationService.Object, _mockShoppingCartService.Object);
        }

        [Fact]
        public void WhenDeconnectionCommandIsCalled_ShouldCallLogOut()
        {
            _customMasterDetailViewModel.DeconnectionCommand.Execute();

            _mockAuthenticationService.Verify(x => x.LogOut(), Times.Once());
        }

        [Fact]
        public void WhenDeconnectionCommandIsCalled_ShouldCallSetNewEmptyShoppingCart()
        {
            _customMasterDetailViewModel.DeconnectionCommand.Execute();

            _mockShoppingCartService.Verify(x => x.SetNewEmptyShoppingCart(), Times.Once());
        }

        [Fact]
        public void WhenDeconnectionCommandIsCalled_ShouldNavigateToMainPage()
        {
            _customMasterDetailViewModel.DeconnectionCommand.Execute();

            _mockNavigationService.Verify(x => x.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(MainPage)), Times.Once());
        }

        [Theory]
        [InlineData("DogShopPage")]
        [InlineData("DogsListPage")]
        [InlineData("MainPage")]
        [InlineData("UserProfilePage")]
        public void OnNavigateCommand_ShouldNavigateToOtherPage(string namePage)
        {
            _customMasterDetailViewModel.OnNavigateCommand.Execute(namePage);

            _mockNavigationService.Verify(x => x.NavigateAsync("CustomMasterDetailPage/NavigationPage/" + namePage), Times.Once());
        }
    }
}
