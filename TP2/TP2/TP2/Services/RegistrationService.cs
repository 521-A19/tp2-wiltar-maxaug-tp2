using System;
using System.Threading.Tasks;
using TP2.Models.Entities;
using Xamarin.Essentials;

namespace TP2.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRepository<User> _repositoryService;
        private readonly ICryptoService _cryptoService;
        private readonly ISecureStorageService _secureStorageService;
        private User _registeredUser;
        public User RegisteredUser { get { return _registeredUser; } }
        private bool _isRegistered;
        public bool IsUserRegistered { get { return _isRegistered; } }

        public RegistrationService(IRepository<User> repositoryService,
                              ICryptoService cryptoService,
                              ISecureStorageService secureStorageService)
        {
            _repositoryService = repositoryService;
            _cryptoService = cryptoService;
            _secureStorageService = secureStorageService;
        }

        public void RegisterUser(string login, string password)
        {
            if (_repositoryService.IsExisting(login))
            {
                _isRegistered = false;
            }
            else
            {
                string salt = _cryptoService.GenerateSalt();
                string key = _cryptoService.GenerateEncryptionKey();
                var newUser = new User()
                {
                    Login = login,
                    HashedPassword = _cryptoService.HashSHA512(password, salt),
                    PasswordSalt = salt,
                    CreditCard = _cryptoService.Encrypt("5162042483342023", key)
                };
                _secureStorageService.SetUserEncryptionKeyAsync(newUser, key);
                _repositoryService.Add(newUser);
                _registeredUser = newUser;
                _isRegistered = true;
            }
        }
    }
}