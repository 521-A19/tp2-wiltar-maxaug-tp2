using Moq;
using Prism.Navigation;
using System;
using TP2.ViewModels;
using TP2.Views;
using TP2.Services;
using Xunit;
using Prism.Services;
using TP2.Externalization;
using System.Collections.Generic;
using TP2.Models.Entities;
using Bogus;
using System.Linq;
using System.ComponentModel;
using TP2.UnitTests.Fixtures;

namespace TP2.UnitTests
{
    public class MainPageViewModelTests : BaseFixture
    {
        private MainPageViewModel _mainPageViewModel;
        private Mock<INavigationService> _mockNavigationService;
        private Mock<IPageDialogService> _mockPageDialogService;
        private Mock<IAuthenticationService> _mockAuthenticationService;
        private List<User> _userList;
        private Fixture _fixture = new Fixture();
        private bool _eventRaisedProperty;

        public MainPageViewModelTests()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _mockPageDialogService = new Mock<IPageDialogService>();
            _mockAuthenticationService = new Mock<IAuthenticationService>();
            _mainPageViewModel = new MainPageViewModel(_mockNavigationService.Object, _mockAuthenticationService.Object, _mockPageDialogService.Object);
            _userList = _fixture.BuildUsersList();
        }

        [Fact]
        public void GoToDogsListCommand_ShouldChangePageToDogsList()
        {
            _mainPageViewModel.GoToDogsListCommand.Execute();

            _mockNavigationService.Verify(x => x.NavigateAsync("CustomMasterDetailPage/NavigationPage/" + nameof(DogsListPage)), Times.Once());
        }

        [Fact]
        public void NavigateToRegisterPageCommand_ShouldNavigateToRegisterPage()
        {
            _mainPageViewModel.NavigateToRegisterPageCommand.Execute();

            _mockNavigationService.Verify(x => x.NavigateAsync("NavigationPage/" + nameof(RegisterPage)), Times.Once());
        }

        [Fact]
        public void AuthenticateCommand_WhenAuthenticationIsNotValid_ShouldNotNavigate()
        {
            //Arrange
            _mainPageViewModel.Login = _userList.First().Login;
            _mainPageViewModel.Password = "189";

            //Act
            _mainPageViewModel.AuthentifivationUserCommand.Execute();
            //Assert
            _mockNavigationService.VerifyNoOtherCalls();
        }

        [Fact]
        public void AuthenticateCommand_WhenAuthenticationIsNotValid_ShouldDisplayAlertToUser()
        {

            //Act
            _mainPageViewModel.AuthentifivationUserCommand.Execute();
            //Assert
            _mockPageDialogService.Verify(x => x.DisplayAlertAsync(UiText.NotValidLogInTitle, UiText.NotValidLogInMessage, "Okay"));
        }

        [Fact]
        public void AuthenticateCommand_WhenAuthenticationIsValid_ShouldNavigateToHomePage()
        {
            //Arrange
            _mockAuthenticationService
              .Setup(a => a.IsUserAuthenticated)
              .Returns(true);
            _mockAuthenticationService
            .Setup(n => n.AuthenticatedUser)
            .Returns(_userList.First());

            //Act
            //_mockAuthenticationService.Object.IsUserAuthenticated = true;
            _mainPageViewModel.AuthentifivationUserCommand.Execute();

            //Assert
            _mockNavigationService.Verify(x => x.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(DogsListPage)), Times.Once());
        }

        [Fact]
        public void AuthenticateCommand_WhenExceptionThrown_ShouldDisplayAlertToUser()
        {
            //Arrange
            _mainPageViewModel.Login = _userList.First().Login;
            _mainPageViewModel.Password = "189";
            _mockAuthenticationService
               .Setup(a => a.IsUserAuthenticated)
               .Throws<Exception>();


            //Act
            _mainPageViewModel.AuthentifivationUserCommand.Execute();

            //Assert
            _mockPageDialogService.Verify(x => x.DisplayAlertAsync(UiText.ErrorExceptionThrowTitle, UiText.ErrorExceptionThrowMessage, "Okay"));
        }

        [Fact]
        public void Login_WhenSetToNewValue_ShouldRaisePropertyChangedEvent()
        {
            _mainPageViewModel.PropertyChanged += RaiseProperty;

            _mainPageViewModel.Login = "TEST";

            Assert.True(_eventRaisedProperty);
        }

        [Fact]
        public void Password_WhenSetToNewValue_ShouldRaisePropertyChangedEvent()
        {
            _mainPageViewModel.PropertyChanged += RaiseProperty;

            _mainPageViewModel.Login = "TEST";

            Assert.True(_eventRaisedProperty);
        }


        private void RaiseProperty(object sender, PropertyChangedEventArgs e)
        {
            _eventRaisedProperty = true;
        }

    }
}

