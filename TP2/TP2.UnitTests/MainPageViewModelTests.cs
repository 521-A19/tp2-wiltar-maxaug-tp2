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

namespace TP2.UnitTests
{
    public class MainPageViewModelTests
    {
        private MainPageViewModel _mainPageViewModel;
        private Mock<INavigationService> _mockNavigationService;
        private Mock<IPageDialogService> _mockPageDialogService;
        private Mock<IAuthenticationService> _mockAuthenticationService;
        private const string NotHashedPassword = "123";
        private const string NotEncryptedCreditCard = "5105105105105100";
        private const string EncryptionKey = "--AnEncryptionKey--";
        private List<User> _userList;
        public MainPageViewModelTests()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _mockPageDialogService = new Mock<IPageDialogService>();
            _mockAuthenticationService = new Mock<IAuthenticationService>();
            _mainPageViewModel = new MainPageViewModel(_mockNavigationService.Object, _mockAuthenticationService.Object, _mockPageDialogService.Object);
            _userList = CreateUserList();
        }

        [Fact]
        public void GoToDogsListCommand_ShouldChangePageToDogsList()
        {
            _mainPageViewModel.GoToDogsListCommand.Execute();

            _mockNavigationService.Verify(x => x.NavigateAsync("NavigationPage/" + nameof(DogsListPage)), Times.Once());
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

            var navigationParameters = new NavigationParameters();
            navigationParameters.Add("data", _userList.First());

            //Act
            //_mockAuthenticationService.Object.IsUserAuthenticated = true;
            _mainPageViewModel.AuthentifivationUserCommand.Execute();
            //Assert
            _mockNavigationService.Verify(x => x.NavigateAsync("/" + nameof(DogsListPage), navigationParameters), Times.Once());
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

        private List<User> CreateUserList()
        {
            var crypto = new CryptoService();
            var salt = crypto.GenerateSalt();
            var userList = new Faker<User>()
                .StrictMode(true)
                .RuleFor(u => u.Login, f => f.Person.Email)
                .RuleFor(u => u.PasswordSalt, f => salt)
                .RuleFor(u => u.CreditCard, f => crypto.Encrypt(NotEncryptedCreditCard, EncryptionKey))
                .RuleFor(u => u.HashedPassword, f => crypto.HashSHA512(NotHashedPassword, salt))
                .RuleFor(u => u.Id, f => f.IndexFaker)
                .Generate(3);
            return userList;
        }


    }
}

