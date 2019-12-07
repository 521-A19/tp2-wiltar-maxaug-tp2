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

namespace TP2.UnitTests
{
    public class DogsListViewModelTests : BaseFixture
    {
        private DogsListViewModel _dogsListViewModel;
        private Mock<IRepository<Dog>> _mockRepositoryService;
        private Mock<INavigationService> _mockNavigationService;
        private List<Dog> _dogList;
        private Fixture _fixture = new Fixture();
        private Dog _newDog = new Dog()
        {
            Name = "Yulu",
            Race = "african",
            Sex = "M",
            Description = "dog",
            ImageUrl = "url",
            Price = 123,
            Id = 9999
        };

        public DogsListViewModelTests()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _mockRepositoryService = new Mock<IRepository<Dog>>();
            _dogList = _fixture.BuildDogsList();
            _dogList.Add(_newDog);
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
        public void SortDogListByName_WhenOrderByBreed_ShouldSortListAndFirstDogChange()
        {
            
            string firstDogNameOfTheList = _dogsListViewModel.Dogs[0].Name;

            _dogsListViewModel.SelectedSortType = 1;
            string newFirstDogNameInTheList = _dogsListViewModel.Dogs[0].Name;

            firstDogNameOfTheList.Should().NotContainEquivalentOf(newFirstDogNameInTheList);
        }

        [Fact]
        public void SortDogListByName_WhenOrderByPrice_ShouldSortListAndFirstDogChange()
        {

            string firstDogNameOfTheList = _dogsListViewModel.Dogs[0].Name;

            _dogsListViewModel.SelectedSortType = 2;
            string newFirstDogNameInTheList = _dogsListViewModel.Dogs[0].Name;

            firstDogNameOfTheList.Should().NotContainEquivalentOf(newFirstDogNameInTheList);
        }
    }
}
