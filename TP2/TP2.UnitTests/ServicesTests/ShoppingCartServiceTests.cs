using Bogus;
using FluentAssertions;
using TP2.Models.Entities;
using TP2.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace TP2.UnitTests.ServicesTests
{
    public class ShoppingCartServiceTests
    {
        private ShoppingCartService _shoppingCartService;
        private const float ANY_DOG_PRICE = (float)299.99;

        public ShoppingCartServiceTests()
        {
            _shoppingCartService = new ShoppingCartService();
        }

        [Fact]
        public void AddDogToTheShoppingCart_ShouldAddToDogListInShoppingCart()
        {
            var dog = CreateFakeDog();

            _shoppingCartService.AddDogToTheShoppingCart(dog);

            _shoppingCartService.ShoppingCartDogList.Should().Contain(dog);
            _shoppingCartService.ShoppingCartDogList[0].Should().BeEquivalentTo(dog);
            _shoppingCartService.ShoppingCartDogList.Should().NotBeEmpty();
        }

        [Fact]
        public void AddDogToTheShoppingCart_ShouldAddDogPriceToTotalPrice()
        {
            var dog = CreateFakeDog();

            _shoppingCartService.AddDogToTheShoppingCart(dog);

            _shoppingCartService.TotalPrice.Should().Be(ANY_DOG_PRICE);
        }

        [Fact]
        public void AddedDog_WhenRemoveDogFromTheShoppingCart_ShouldRemoveDogFromTheShoppingCart()
        {
            var dog = CreateFakeDog();
            _shoppingCartService.AddDogToTheShoppingCart(dog);

            _shoppingCartService.RemoveDogFromTheShoppingCart(dog);

            _shoppingCartService.ShoppingCartDogList.Should().BeEmpty();
        }

        [Fact]
        public void AddedDog_WhenRemoveDogFromTheShoppingCart_ShouldSubtractDogPriceFromTotalPrice()
        {
            var dog = CreateFakeDog();
            _shoppingCartService.AddDogToTheShoppingCart(dog);

            _shoppingCartService.RemoveDogFromTheShoppingCart(dog);

            _shoppingCartService.TotalPrice.Should().Be(0);
        }

        [Fact]
        public void DogList_Contains_ShouldReturnTrueIfFound()
        {
            var dogs = CreateDogList();
            foreach(Dog cur in dogs) _shoppingCartService.AddDogToTheShoppingCart(cur); //No Set

            foreach (Dog cur in _shoppingCartService.ShoppingCartDogList) _shoppingCartService.Contains(cur.Id).Should().BeTrue();
        }

        private Dog CreateFakeDog()
        {
            var fakeDog = new Faker<Dog>()
                .StrictMode(true)
                .RuleFor(u => u.Id, f => 0)
                .RuleFor(u => u.Name, f => "Rex")
                .RuleFor(u => u.Race, f => "Husky")
                .RuleFor(u => u.Sex, f => "Male")
                .RuleFor(u => u.Description, f => "Dog")
                .RuleFor(u => u.ImageUrl, f => "url")
                .RuleFor(u => u.Price, f => ANY_DOG_PRICE)
                .Generate();
            return fakeDog;
        }

        private List<Dog> CreateDogList()
        {
            var dogList = new Faker<Dog>()
                .StrictMode(true)
                .RuleFor(u => u.Name, f => f.Person.FirstName)
                .RuleFor(u => u.Price, f => (float)299.99)
                .RuleFor(u => u.Race, f => "Husky")
                .RuleFor(u => u.Description, f => "Dog")
                .RuleFor(u => u.Sex, f => f.Person.Gender.ToString())
                .RuleFor(u => u.ImageUrl, f => "url")
                .RuleFor(u => u.Id, f => f.IndexFaker)
                .Generate(3);
            return dogList;
        }
    }
}