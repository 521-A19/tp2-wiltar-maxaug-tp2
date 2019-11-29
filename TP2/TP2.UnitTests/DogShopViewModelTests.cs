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

namespace TP2.UnitTests
{
    public class DogShopViewModelTests
    {
        private DogShopViewModel _dogShopViewModel;
        private Mock<INavigationService> _mockNavigationService;
        private Mock<IAuthenticationService> _mockAuthenticationService;
        private Mock<IPageDialogService> _mockPageDialogService;
        private Mock<IRepository<Dog>> _mockRepositoryService;
        private bool _eventRaisedProperty;
         
        public DogShopViewModelTests()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _mockAuthenticationService = new Mock<IAuthenticationService>();
            _mockPageDialogService = new Mock<IPageDialogService>();
            _mockRepositoryService = new Mock<IRepository<Dog>>();
            //_mockRepositoryService.Setup(r => r.GetAll()).Returns(_dogList);
            _dogShopViewModel = new DogShopViewModel(_mockNavigationService.Object, _mockAuthenticationService.Object, _mockPageDialogService.Object, _mockRepositoryService.Object);
        }
    
        [Fact]
        public void UserIsNotConnected_OnNavigatedPage_ShouldDisplayAlertMessage()
        {
            //_mockUserRepository.Setup(r => r.IsExisting(It.IsAny<string>())).Returns(false);
            _mockPageDialogService.Verify(x => x.DisplayAlertAsync(UiText.WARNING, UiText.USER_NOT_CONNECTED, UiText.CONFIRM));
        }

        [Fact]
        public void UserIsNotConnected_OnNavigatedPage_ButtonToDogRegisterPageShouldNotBeVisible()
        {
            _dogShopViewModel.IsButtonToAddNewDogPageVisible.Should().BeFalse();
        }

        [Fact]
        public void UserIsConnectedWithNoCurrentDog_OnNavigatedPage_ButtonToDogRegisterPageShouldBeVisible()
        {
            _mockAuthenticationService.Setup(r => r.IsUserAuthenticated).Returns(true);
            _mockAuthenticationService.Setup(r => r.AuthenticatedUser).Returns(CreateFakeUser());

            _dogShopViewModel = new DogShopViewModel(_mockNavigationService.Object, _mockAuthenticationService.Object, _mockPageDialogService.Object, _mockRepositoryService.Object);
            
            _dogShopViewModel.IsButtonToAddNewDogPageVisible.Should().BeTrue();
        }

        [Fact]
        public void UserIsConnectedWithNoCurrentDog_OnNavigatedPage_ShouldDisplayAlertMessage()
        {
            _mockAuthenticationService.Setup(r => r.IsUserAuthenticated).Returns(true);
            _mockAuthenticationService.Setup(r => r.AuthenticatedUser).Returns(CreateFakeUser());

            _dogShopViewModel = new DogShopViewModel(_mockNavigationService.Object, _mockAuthenticationService.Object, _mockPageDialogService.Object, _mockRepositoryService.Object);

            _mockPageDialogService.Verify(x => x.DisplayAlertAsync(UiText.WARNING, UiText.NO_CURRENT_DOG, UiText.CONFIRM));
        }

        [Fact]
        public void MyDog_ModifyMyDog_ShouldDisplayAlert()
        {
            _mockAuthenticationService.Setup(r => r.IsUserAuthenticated).Returns(true);
            _mockAuthenticationService.Setup(r => r.AuthenticatedUser).Returns(CreateFakeUser());

            _dogShopViewModel.ModifyDogInformations.Execute();

            _mockPageDialogService.Verify(x => x.DisplayAlertAsync(UiText.SUCCESS, UiText.DOG_INFO_MODIFIED, UiText.CONFIRM));
        }

        [Fact]
        public void NavigateToAddNewDogPageCommand_ShouldNavigateToAddNewDogPage()
        {
            _dogShopViewModel.NavigateToAddNewDogPageCommand.Execute();

            _mockNavigationService.Verify(x => x.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(AddNewDogPage)), Times.Once());
        }

        /*
        [Fact]
        public void MyDog_WhenSetToNewValue_ShouldRaisePropertyChangedEvent()
        {
            _dogShopViewModel.PropertyChanged += RaiseProperty;

            _dogShopViewModel.MyDog = new Faker<Dog>();

            Assert.True(_eventRaisedProperty);
        }*/

        [Fact]
        public void IsButtonToDogRegisterVisible_WhenSetToNewValue_ShouldRaisePropertyChangedEvent()
        {
            _dogShopViewModel.PropertyChanged += RaiseProperty;

            _dogShopViewModel.IsButtonToAddNewDogPageVisible = true;

            Assert.True(_eventRaisedProperty);
        }

        private void RaiseProperty(object sender, PropertyChangedEventArgs e)
        {
            _eventRaisedProperty = true;
        }


        private Faker<User> CreateFakeUser()
        {
            var fakeUser = new Faker<User>()
                .StrictMode(true)
                .RuleFor(u => u.Id, f => f.IndexFaker)
                .RuleFor(u => u.Login, f => f.Person.Email)
                .RuleFor(u => u.CreditCard, f => "556468486")
                .RuleFor(u => u.HashedPassword, f => "264531")
                .RuleFor(u => u.PasswordSalt, f => "adskadk")
                .RuleFor(u => u.DogId, f => -1);
            return fakeUser;
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
