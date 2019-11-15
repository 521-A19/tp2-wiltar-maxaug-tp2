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
    public class SecureStorageServiceTests
    {
        private ISecureStorageService _secureStorageService;
        private ICryptoService _cryptoService;
        private User _user;
        private const string NotHashedPassword = "Qwertyuiop1";
        private const string NotEncryptedCreditCard = "5105105105105100";
        private const string EncryptionKey = "--AnEncryptionKey--";

        public SecureStorageServiceTests()
        {
            _secureStorageService = new SecureStorageService();
            _cryptoService = new CryptoService();
            var salt = _cryptoService.GenerateSalt();
            _user = new User()
            {
                Login = "testing@email.com",
                HashedPassword = _cryptoService.HashSHA512("456", salt),
                PasswordSalt = salt,
                CreditCard = _cryptoService.Encrypt("5162042483342023", EncryptionKey)
            };
        }

        [Fact]
        public async Task EmptyPassword_WhenLogin_ShouldThrowNewExceptionAsync()
        {
            await _secureStorageService.SetUserEncryptionKeyAsync(_user, EncryptionKey);

            string key = await _secureStorageService.GetUserEncryptionKeyAsync(_user);
            Assert.Equal("sdada", key);
        }
    }
}