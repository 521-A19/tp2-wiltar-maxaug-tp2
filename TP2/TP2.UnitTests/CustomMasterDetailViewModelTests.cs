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
        public void ConnecterUser_WhenLogOut_ShouldNavigateToMainPage()
        {
            _customMasterDetailViewModel.OnNavigateCommand.Execute("/CustomMasterDetailPage/NavigationPage/DogShopPage");

            _mockNavigationService.Verify(x => x.NavigateAsync("/CustomMasterDetailPage/NavigationPage/DogShopPage"), Times.Once());
        }

        [Fact]
        public void OnNavigateCommand_ShouldNavigateToOtherPage()
        {
            _customMasterDetailViewModel.OnNavigateCommand.Execute("/CustomMasterDetailPage/NavigationPage/DogShopPage");

            _mockNavigationService.Verify(x => x.NavigateAsync("/CustomMasterDetailPage/NavigationPage/DogShopPage"), Times.Once());
        }

        [Fact]
        public void DeconnectionCommand_ShouldNavigateToMainPage()
        {
            _customMasterDetailViewModel.DeconnectionCommand.Execute();

            _mockNavigationService.Verify(x => x.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(MainPage)), Times.Once());
        }

        [Fact]
        public void ShowUserProfileCommand_ShouldNavigateToUserProfilePage()
        {
            _customMasterDetailViewModel.ShowUserProfileCommand.Execute();

            _mockNavigationService.Verify(x => x.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(UserProfilePage)), Times.Once());
        }
    }

}
