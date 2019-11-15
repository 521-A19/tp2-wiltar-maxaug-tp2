using System;
using System.Collections.Generic;
using TP2.Externalization;
using TP2.Models.Entities;

namespace TP2.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private bool _isLogin;
        private User _user;
        private IEnumerable<User> _listUsers;
        private readonly ICryptoService _cryptoService;
        private readonly IRepository<User> _repositoryService;

        public AuthenticationService(IRepository<User> repositoryService,
                              ICryptoService cryptoService)
        {
            _repositoryService = repositoryService;
            _cryptoService = cryptoService;
        }

        public User AuthenticatedUser => _user;
        //private User _authenticatedUser;
        //public User AuthenticatedUser{get{return _authenticatedUser}}

        public bool IsUserAuthenticated => _isLogin;

        public void LogIn(string login, string password)
        {
            if (String.IsNullOrWhiteSpace(login) || String.IsNullOrWhiteSpace(password))
            {
                throw new Exception();
            }
            if (_isLogin)
            {
                throw new Exception(UiText.USER_ALREADY_CONNECTED);
            }

            _listUsers = _repositoryService.GetAll(); //.FirstOrDefault(x => x.Login == login);
            foreach (var cur in _listUsers)
            {
                string hashedPassword = _cryptoService.HashSHA512(password, cur.PasswordSalt);
                if (cur.Login == login && cur.HashedPassword == hashedPassword)
                {
                    _user = cur;
                    _isLogin = true;
                    return;
                }
            }
        }
    }
}