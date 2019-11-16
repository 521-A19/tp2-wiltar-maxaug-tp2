using Bogus;
using FluentAssertions;
using TP2.Models.Entities;
using TP2.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xunit;

namespace TP2.UnitTests.ServicesTests
{
    public class AuthenticationServiceTests
    {
        private AuthenticationService _authenticateService;
        private Mock<IRepository<User>> _mockUserRepository;
        private ICryptoService _cryptoService; // Mock non nécessaire
        const string EMPTY_STRING_VALUE = "";
        private const string NotHashedPassword = "Qwertyuiop1";
        private const string NotEncryptedCreditCard = "5105105105105100";
        private const string EncryptionKey = "--AnEncryptionKey--";
        private List<User> _userList;


        public AuthenticationServiceTests()
        {
            _mockUserRepository = new Mock<IRepository<User>>();
            _cryptoService = new CryptoService();
            _authenticateService = new AuthenticationService(_mockUserRepository.Object, _cryptoService);
            _userList = CreateUserList();
            _mockUserRepository
                .Setup(r => r.GetAll())
                .Returns(_userList);
        }

        [Fact]
        public void EmptyPassword_WhenLogin_ShouldThrowNewException()
        {
            const string VALID_PASSWORD = NotHashedPassword;

            _authenticateService.Invoking(y => y.LogIn(EMPTY_STRING_VALUE, VALID_PASSWORD)).Should().
                Throw<Exception>();
        }

        [Fact]
        public void EmptyEmail_WhenLogin_ShouldThrowNewException()
        {
            string VALID_LOGIN = _userList[0].Login;

            _authenticateService.Invoking(y => y.LogIn(VALID_LOGIN, EMPTY_STRING_VALUE)).Should().
                Throw<Exception>();
        }


        [Fact]
        public void IsUserAuthenticated_WhenLoginAgain_ShouldThrowNewException()
        {
            string VALID_LOGIN = _userList[0].Login;
            const string VALID_PASSWORD = NotHashedPassword;
            _authenticateService.LogIn(VALID_LOGIN, VALID_PASSWORD);

            _authenticateService.Invoking(y => y.LogIn(VALID_LOGIN, VALID_PASSWORD)).Should().
                Throw<Exception>();
            //Exception ex = Assert.Throws<Exception>(() => _authenticateService.LogIn(VALID_LOGIN, VALID_PASSWORD)
            //ex.Message.Should().BeEquivalentTo(UiText.ExceptionAUserIsAlreadyAuthenticated
        }

        [Fact]
        public void AuthenticateService_IsUserAuthenticated_ShouldBeSetToFalse()
        {
            _authenticateService.IsUserAuthenticated.Should().BeFalse();
        }

        [Fact]
        public void ValidEmailAndPassword_Login_ShouldSetIsUserAuthenticatedToTrue()
        {
            string VALID_LOGIN = _userList[0].Login;
            const string VALID_PASSWORD = NotHashedPassword;
            _authenticateService.LogIn(VALID_LOGIN, VALID_PASSWORD);

            _authenticateService.IsUserAuthenticated.Should().BeTrue();
        }

        [Fact]
        public void Login_ShouldSetAuthenticatedUser()
        {
            string VALID_LOGIN = _userList[0].Login;
            const string VALID_PASSWORD = NotHashedPassword;

            _authenticateService.LogIn(VALID_LOGIN, VALID_PASSWORD);

            _authenticateService.AuthenticatedUser.Should().Be(_userList[0]);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void ValidEmailAndPassword_Login_ShouldSetAuthenticatedUser(int indexOfUserList)
        {
            string VALID_LOGIN = _userList[indexOfUserList].Login;
            const string VALID_PASSWORD = NotHashedPassword;
            _authenticateService.LogIn(VALID_LOGIN, VALID_PASSWORD);

            _authenticateService.AuthenticatedUser.Should().Be(_userList[indexOfUserList]);
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