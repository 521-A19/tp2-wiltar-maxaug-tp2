using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TP2.Externalization;
using TP2.Models.Entities;
using TP2.Services;

namespace TP2.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository<User> _repository;
        private readonly ICryptoService _cryptoService;
        public bool _isUserAuthenticated = false;
        private User _authenticatedUser;

        public bool IsUserAuthenticated
        {
            get { return _isUserAuthenticated; }
            set
            {
                _isUserAuthenticated = value;
            }
        }

        public User AuthenticatedUser
        {
            get { return _authenticatedUser; }
            set
            {
                _authenticatedUser = value;
            }
        }

        public AuthenticationService(IRepository<User> repository, ICryptoService cryptoService)
        {
            _cryptoService = cryptoService;
            _repository = repository;
        }

        //IAuthenticationService

        public void LogIn(string login, string password)
        {
            if (AuthenticatedUser != null)
            {
                throw new Exception(UiText.ExceptionUserIsAlreadyLogIn);
            }

            Authenticate(login, password);
        }

        public void Authenticate(string login, string password)
        {
            var userdata = _repository.GetAll().FirstOrDefault(x => x.Login == login);
            
            if (userdata != null)
            {
                var passwordHash = _cryptoService.HashSHA512(password, userdata.PasswordSalt);
                if (passwordHash == userdata.HashedPassword)
                {
                    IsUserAuthenticated = true;
                    AuthenticatedUser = userdata;
                }
                else
                {
                    AuthenticatedUser = null;
                }
            }
        }
    }
}
