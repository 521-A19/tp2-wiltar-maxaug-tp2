using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using TP2.Models.Entities;
using TP2.Services;
using Xunit;

namespace TP2.UnitTests.Fixture
{
    public class Fixture 
    {

        private Faker<User> _userFaker;
        private Faker<Dog> _dogFaker;

        private const string NotHashedPassword = "Qwertyuiop1";

        private const string NotEncryptedCreditCard = "5105105105105100";
        public string Last4DigitCreditCard => NotEncryptedCreditCard.Substring(NotEncryptedCreditCard.Length - 4);
        public string EncryptionKey => "--AnEncryptionKey--";

        public Fixture()
        {
            InitializeFakersWithRules();
        }

        public List<Dog> BuildDogsList()
        {
            return _dogFaker.Generate(3);
        }

        public List<User> BuildUsersList()
        {

            return _userFaker.Generate(3);
        }

        private void InitializeFakersWithRules()
        {
            var crypto = new CryptoService();
            var salt = crypto.GenerateSalt();
            _userFaker = new Faker<User>()
                .StrictMode(true)
                .RuleFor(u => u.Login, f => f.Person.Email)
                .RuleFor(u => u.PasswordSalt, f => salt)
                .RuleFor(u => u.CreditCard, f => crypto.Encrypt(NotEncryptedCreditCard, EncryptionKey))
                .RuleFor(u => u.HashedPassword, f => crypto.HashSHA512(NotHashedPassword, salt))
                .RuleFor(u => u.Id, f => f.IndexFaker)
                .RuleFor(u => u.DogId, f => 1);

            _dogFaker = new Faker<Dog>()
              .StrictMode(true)
              .RuleFor(u => u.Name, f => f.Person.FirstName)
              .RuleFor(u => u.Price, f => (float)299.99)
              .RuleFor(u => u.Race, f => "Husky")
              .RuleFor(u => u.Description, f => "Dog")
              .RuleFor(u => u.Sex, f => f.Person.Gender.ToString())
              .RuleFor(u => u.ImageUrl, f => "url")
              .RuleFor(u => u.Id, f => f.IndexFaker);
        }

       
    }

}

