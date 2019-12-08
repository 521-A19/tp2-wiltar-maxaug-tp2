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
using TP2.Externalization;
using TP2.UnitTests.Fixtures;

namespace TP2.UnitTests
{
    public class DogShopViewModelTests : BaseFixture
    {
        private DogShopViewModel _dogShopViewModel;
        private Mock<INavigationService> _mockNavigationService;
        private Mock<IAuthenticationService> _mockAuthenticationService;
        private Mock<IPageDialogService> _mockPageDialogService;
        private Mock<IRepository<Dog>> _mockRepositoryService;
        private List<User> _userList;
        private List<Dog> _dogList;
        private Fixture _fixture = new Fixture();

        public DogShopViewModelTests()
        {
            _userList = _fixture.BuildUsersList();
            _dogList = _fixture.BuildDogsList();
            _mockNavigationService = new Mock<INavigationService>();
            _mockAuthenticationService = new Mock<IAuthenticationService>();
            _mockPageDialogService = new Mock<IPageDialogService>();
            _mockRepositoryService = new Mock<IRepository<Dog>>();
            //_mockRepositoryService.Setup(r => r.GetAll()).Returns(_dogList);
            _dogShopViewModel = new DogShopViewModel(_mockNavigationService.Object, _mockAuthenticationService.Object, _mockPageDialogService.Object, _mockRepositoryService.Object);
        }

        [Fact]
        public void AuthenticatedUserHasNoDog_OnNavigatedTo_UserHasAnyDogShouldBeFalse()
        {
            _userList[0].DogId = -1;
            _mockAuthenticationService.Setup(r => r.AuthenticatedUser).Returns(_userList[0]);
            var navigationParameters = new NavigationParameters();

            _dogShopViewModel.OnNavigatedTo(navigationParameters);

            _dogShopViewModel.UserHasAnyDog.Should().BeFalse();
        }

        [Fact]
        public void AuthenticatedUserHasNoDog_OnNavigatedTo_ShouldDisplayAlertMessage()
        {
            _userList[0].DogId = -1;
            _mockAuthenticationService.Setup(r => r.AuthenticatedUser).Returns(_userList[0]);
            var navigationParameters = new NavigationParameters();

            _dogShopViewModel.OnNavigatedTo(navigationParameters);

            _mockPageDialogService.Verify(x => x.DisplayAlertAsync(UiText.WARNING, UiText.NO_CURRENT_DOG, UiText.CONFIRM));
        }

        [Fact]
        public void AuthenticatedUserHasADog_OnNavigatedTo_UserHasAnyDogShouldGetTrue()
        {
            _mockAuthenticationService.Setup(r => r.AuthenticatedUser).Returns(_userList[0]);
            var dog = _dogList[0];
            _mockRepositoryService.Setup(r => r.GetById(1)).Returns(dog);
            var navigationParameters = new NavigationParameters();

            _dogShopViewModel.OnNavigatedTo(navigationParameters);

            _dogShopViewModel.UserHasAnyDog.Should().BeTrue();
        }

        [Fact]
        public void AuthenticatedUserHasADog_OnNavigatedTo_MyDogShouldBeInstantiated()
        {
            _mockAuthenticationService.Setup(r => r.AuthenticatedUser).Returns(_userList[0]);
            var dog = _dogList[0];
            _mockRepositoryService.Setup(r => r.GetById(1)).Returns(dog);
            var navigationParameters = new NavigationParameters();

            _dogShopViewModel.OnNavigatedTo(navigationParameters);

            _dogShopViewModel.MyDog.Should().NotBeNull();
        }


        [Fact]
        public void MyDog_ModifyMyDog_ShouldCallUpdateMethodAndDisplayAlertMessage()
        {
            _mockAuthenticationService.Setup(r => r.IsUserAuthenticated).Returns(true);
            _mockAuthenticationService.Setup(r => r.AuthenticatedUser).Returns(_userList[0]);

            _dogShopViewModel.ModifyDogInformations.Execute();

            _mockRepositoryService.Verify(x => x.Update(It.IsAny<Dog>()), Times.Once());
            _mockPageDialogService.Verify(x => x.DisplayAlertAsync(UiText.SUCCESS, UiText.DOG_INFO_MODIFIED, UiText.CONFIRM));
        }

        [Fact]
        public void NavigateToAddNewDogPageCommand_ShouldNavigateToAddNewDogPage()
        {
            _dogShopViewModel.NavigateToAddNewDogPageCommand.Execute();

            _mockNavigationService.Verify(x => x.NavigateAsync("CustomMasterDetailPage/NavigationPage/" + nameof(AddNewDogPage)), Times.Once());
        }

        
        [Fact]
        public void MyDog_WhenSetToNewValue_ShouldRaisePropertyChangedEvent()
        {
            _dogShopViewModel.PropertyChanged += _fixture.RaiseProperty;

            _dogShopViewModel.MyDog = new Faker<Dog>();

            Assert.True(_fixture._eventRaisedProperty);
        }
    }
}
