using Moq;
using Prism.Navigation;
using Prism.Services;
using System;
using TP2.Models.Entities;
using TP2.Services;
using TP2.ViewModels;
using TP2.Views;
using Xunit;
using Bogus;
using FluentAssertions;
using System.Collections.Generic;
using TP2.UnitTests.Fixtures;
using System.Collections.ObjectModel;

namespace TP2.UnitTests
{
    public class DogsListViewModelTests : BaseFixture
    {
        private DogsListViewModel _dogsListViewModel;
        private Mock<IRepository<Dog>> _mockRepositoryService;
        private Mock<INavigationService> _mockNavigationService;
        private List<Dog> _dogList;
        private Fixture _fixture = new Fixture();
  

        public DogsListViewModelTests()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _mockRepositoryService = new Mock<IRepository<Dog>>();
            _dogList = _fixture.BuildDogsList();
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

            _mockNavigationService.Verify(x => x.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(DogDetailPage), It.IsAny<INavigationParameters>()), Times.Once());
        }

        [Fact]
        public void OrderDogsByName_ShouldSortDogsNameAlphabetically()
        {
            _dogList[0].Name = "Zzzz";
            _dogsListViewModel = new DogsListViewModel(_mockNavigationService.Object, _mockRepositoryService.Object);
            Dog firstDogNameOfTheList = _dogsListViewModel.Dogs[0];

            _dogsListViewModel.SelectedSortType = 0;
            Dog newFirstDogNameInTheList = _dogsListViewModel.Dogs[0];

            firstDogNameOfTheList.Should().NotBe(newFirstDogNameInTheList);
        }

        [Fact]
        public void OrderDogsByRace_ShouldSortDogsRaceAlphabetically()
        {
            _dogList[0].Race = "Zzzz";
            _dogsListViewModel = new DogsListViewModel(_mockNavigationService.Object, _mockRepositoryService.Object);
            Dog firstDogNameOfTheList = _dogsListViewModel.Dogs[0];

            _dogsListViewModel.SelectedSortType = 1;
            Dog newFirstDogNameInTheList = _dogsListViewModel.Dogs[0];

            firstDogNameOfTheList.Should().NotBe(newFirstDogNameInTheList);
        }

        [Fact]
        public void OrderDogsByPrice_ShouldSortDogsPriceFromLowestToHighest()
        {
            _dogList[0].Price = (float)999.99;
            _dogsListViewModel = new DogsListViewModel(_mockNavigationService.Object, _mockRepositoryService.Object);
            Dog firstDogNameOfTheList = _dogsListViewModel.Dogs[0];

            _dogsListViewModel.SelectedSortType = 2;
            Dog newFirstDogNameInTheList = _dogsListViewModel.Dogs[0];

            firstDogNameOfTheList.Should().NotBe(newFirstDogNameInTheList);
        }

        [Fact]
        public void SelectedSortType_WhenSetToNewValue_ShouldRaisePropertyChangedEvent()
        {
            _dogsListViewModel.PropertyChanged += _fixture.RaiseProperty;

            _dogsListViewModel.SelectedSortType = 1;

            Assert.True(_fixture._eventRaisedProperty);
        }

        [Fact]
        public void Dogs_WhenSetToNewValue_ShouldRaisePropertyChangedEvent()
        {
            _dogsListViewModel.PropertyChanged += _fixture.RaiseProperty;

            _dogsListViewModel.Dogs = new ObservableCollection<Dog>(_dogList);

            Assert.True(_fixture._eventRaisedProperty);
        }
    }
}
