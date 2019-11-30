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

namespace TP2.UnitTests
{
    public class CostomMasterDetailViewModelTests
    {
        private CustomMasterDetailViewModel _customMasterDetailViewModel;
        private Mock<INavigationService> _mockNavigationService;
        private Mock<IAuthenticationService> _mockAuthenticationService;

        public CostomMasterDetailViewModelTests()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _mockAuthenticationService = new Mock<IAuthenticationService>();
            _customMasterDetailViewModel = new CustomMasterDetailViewModel(_mockNavigationService.Object, _mockAuthenticationService.Object);
        }

        [Fact]
        public void ConnecterUser_DeconnectionCommand_ShouldCallLogOut()
        {
            _customMasterDetailViewModel.DeconnectionCommand.Execute();

            _mockAuthenticationService.Verify(x => x.LogOut(), Times.Once());
        }

        [Fact]
        public void ConnecterUser_DeconnectionCommand_ShouldNavigateToMainPage()
        {
            _customMasterDetailViewModel.DeconnectionCommand.Execute();

            _mockNavigationService.Verify(x => x.NavigateAsync("CustomMasterDetailPage/NavigationPage/MainPage"), Times.Once());
        }

        [Theory]
        [InlineData("DogShopPage")]
        [InlineData("DogsListPage")]
        [InlineData("MainPage")]
        public void OnNavigateCommand_ShouldNavigateToOtherPage(string namePage)
        {
            _customMasterDetailViewModel.OnNavigateCommand.Execute(namePage);

            _mockNavigationService.Verify(x => x.NavigateAsync("CustomMasterDetailPage/NavigationPage/" + namePage), Times.Once());
        }
    }
}
