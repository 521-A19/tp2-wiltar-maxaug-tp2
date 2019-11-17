using Moq;
using Prism.Navigation;
using System;
using TP2.Models.Entities;
using TP2.Services;
using TP2.ViewModels;
using TP2.Views;
using Xunit;
using Bogus;
using FluentAssertions;
using System.Collections.Generic;

namespace TP2.UnitTests
{
    public class DogsListViewModelTests
    {
        private DogsListViewModel _dogsListViewModel;
        private Mock<IRepository<Dog>> _mockRepositoryService;
        private Mock<INavigationService> _mockNavigationService;
        private List<Dog> _dogList;

        public DogsListViewModelTests()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _mockRepositoryService = new Mock<IRepository<Dog>>();
            _dogList = CreateDogList();
            _mockRepositoryService
                .Setup(r => r.GetAll())
                .Returns(_dogList);
            _dogsListViewModel = new DogsListViewModel(_mockNavigationService.Object, _mockRepositoryService.Object);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void OnArrival_DogsObservableListShouldBeLoaded(int indexOfDogList)
        {
            Assert.Contains(_dogList[indexOfDogList], _dogsListViewModel.Dogs);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void OnSelectedDog_ShouldGoToDogDetailPage(int indexOfDogList)
        {
            _dogsListViewModel.SelectedDog = _dogList[indexOfDogList];

            _mockNavigationService.Verify(x => x.NavigateAsync(nameof(DogDetailPage), It.IsAny<INavigationParameters>()), Times.Once());
        }

        [Fact]
        public void GoToDogShopCommand_ShouldChangePageToDogShop()
        {
            _dogsListViewModel.GoToDogShopCommand.Execute();

            _mockNavigationService.Verify(x => x.NavigateAsync("DogsListPage/" + nameof(DogShopPage)), Times.Once());
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
