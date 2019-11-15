using Bogus;
using FluentAssertions;
using TP2.Models.Entities;
using TP2.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xunit;

namespace Lab8.UnitTests.ServicesTests
{
    public class RegistrationServiceTests
    {
        private IRegistrationService _registrationService;
        private Mock<IRepository<User>> _mockUserRepository;
        private ICryptoService _cryptoService; // Mock non nécessaire
        private readonly Mock<ISecureStorageService> _secureStorageService;
        const string EMPTY_STRING_VALUE = "";
        private const string NotHashedPassword = "Qwertyuiop1";
        private const string NotEncryptedCreditCard = "5105105105105100";
        private const string EncryptionKey = "--AnEncryptionKey--";
        private List<User> _userList;


        public RegistrationServiceTests()
        {
            _mockUserRepository = new Mock<IRepository<User>>();
            _cryptoService = new CryptoService();
            _secureStorageService = new Mock<ISecureStorageService>();
            _registrationService = new RegistrationService(_mockUserRepository.Object, _cryptoService, _secureStorageService.Object);
            _userList = CreateUserList();
            _mockUserRepository
                .Setup(r => r.GetAll())
                .Returns(_userList);
        }

        [Fact]
        public void ExistingUser_WhenRegisterUser_ShouldSetIsRegisteredToFalse()
        {
            _mockUserRepository.Setup(r => r.IsExisting(It.IsAny<string>())).Returns(true);

            _registrationService.RegisterUser(It.IsAny<string>(), It.IsAny<string>());

            _registrationService.IsUserRegistered.Should().BeFalse();
            _mockUserRepository.Verify(x => x.IsExisting(It.IsAny<string>()), Times.Once());
        }


        [Fact]
        public void NonExistingUser_WhenRegisterUser_ShouldSetIsRegisteredToTrue()
        {
            _mockUserRepository.Setup(r => r.IsExisting(It.IsAny<string>())).Returns(false);

            _registrationService.RegisterUser(It.IsAny<string>(), It.IsAny<string>());

            _registrationService.IsUserRegistered.Should().BeTrue();
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