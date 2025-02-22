﻿using Bogus;
using FluentAssertions;
using Moq;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using TP2.Models.Entities;
using TP2.Services;
using TP2.UnitTests.Fixtures;
using TP2.ViewModels;
using TP2.Views;
using Xunit;

namespace TP2.UnitTests
{
    public class UserProfileViewModelTests : BaseFixture
    {
        private List<User> _userList;
        private List<Dog> _dogList;
        UserProfileViewModel _userProfileViewModel;
        private Mock<INavigationService> _mockNavigationService;
        private Mock<IRepository<Dog>> _mockRepository;
        private Mock<IAuthenticationService> _mockAuthentification;
        private Mock<IRepository<User>> _mockUserRepository;
        private Fixture _fixture = new Fixture();
        public UserProfileViewModelTests()
        {
            _fixture.BuildUsersList();
            _userList = _fixture.BuildUsersList();
            _dogList = _fixture.BuildDogsList();
            _mockNavigationService = new Mock<INavigationService>();
            _mockRepository = new Mock<IRepository<Dog>>();
            _mockAuthentification = new Mock<IAuthenticationService>();
            _mockUserRepository = new Mock<IRepository<User>>();
            _mockAuthentification.Setup(n => n.AuthenticatedUser).Returns(_userList[0]);
            _userProfileViewModel = new UserProfileViewModel(_mockNavigationService.Object, _mockAuthentification.Object, _mockRepository.Object, _mockUserRepository.Object);
        }

        [Fact]
        public void AuthenticatedUserHasOneDog_IsDeleteMyDogButtonVisibleShouldBeTrue()
        {
            _userProfileViewModel.MyDog = _dogList[0];

            _mockAuthentification.Setup(r => r.AuthenticatedUser).Returns(_userList[0]);
            _userProfileViewModel = new UserProfileViewModel(_mockNavigationService.Object, _mockAuthentification.Object, _mockRepository.Object, _mockUserRepository.Object);
            _userProfileViewModel.IsDeleteMyDogButtonVisible.Should().BeTrue();
        }

        [Fact]
        public void AuthenticatedUserHasNoDog_IsDeleteMyDogButtonVisibleShouldBeFalse()
        {
            _userList[0].DogId = -1;
            _mockAuthentification.Setup(r => r.AuthenticatedUser).Returns(_userList[0]);
            _userProfileViewModel = new UserProfileViewModel(_mockNavigationService.Object, _mockAuthentification.Object, _mockRepository.Object, _mockUserRepository.Object);

            _userProfileViewModel.IsDeleteMyDogButtonVisible.Should().BeFalse();
        }

        [Fact]
        public void UserHasDog_DeleteDogShopCommand_ShouldSetUserDogIdToEmpty()
        {
            const int EXPECTED_USER_DOG_ID = -1;
            _userProfileViewModel.MyDog = _dogList[0];

            _userProfileViewModel.DeleteDogShopCommand.Execute();

            _userProfileViewModel.GetAuthenticatedUser.DogId.Should().Equals(EXPECTED_USER_DOG_ID);
        }

        [Fact]
        public void DeleteDogShopCommand_ShouldCallDeleteFromDogRepository()
        {
            _userProfileViewModel.DeleteDogShopCommand.Execute();

            _mockRepository.Verify(x => x.Delete(It.IsAny<Dog>()), Times.Once());
        }

        [Fact]
        public void DeleteDogShopCommand_ShouldCallUpdateFromUserRepository()
        {
            _userProfileViewModel.DeleteDogShopCommand.Execute();

            _mockUserRepository.Verify(x => x.Update(It.IsAny<User>()), Times.Once());
        }

        [Fact]
        public void DeleteDogShopCommand_WhenUserHaveADogAndDeleteDog_ShouldNavigateToDogListPage()
        {
            _userProfileViewModel.MyDog = _dogList[0];

            _userProfileViewModel.DeleteDogShopCommand.Execute();

            _mockNavigationService.Verify(x => x.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(DogsListPage)), Times.Once());
        }
    }
}
