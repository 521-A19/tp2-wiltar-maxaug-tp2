using FluentAssertions;
using Moq;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using TP2.Externalization;
using TP2.Models.Entities;
using TP2.Services;
using TP2.ViewModels;
using TP2.Views;
using Xunit;

namespace TP2.UnitTests
{
    public class AddNewDogViewModelTests
    {
        AddNewDogViewModel _addNewDogViewModel;
        private Mock<INavigationService> _mockNavigationService;
        private Mock<IPageDialogService> _mockPageDialogService;
        private Mock<IRepository<Dog>> _mockRepository;
        private IDogApiService _dogApiService;

        public AddNewDogViewModelTests()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _mockPageDialogService = new Mock<IPageDialogService>();
            _mockRepository = new Mock<IRepository<Dog>>();
            _dogApiService = new DogApiService();
            _addNewDogViewModel = new AddNewDogViewModel(_mockNavigationService.Object, _dogApiService, _mockRepository.Object, _mockPageDialogService.Object);
        }

        [Fact]
        public void AddNewDogCommand_WhenAllDogAttributIsHonored_ShouldNavigateToDogsList()
        {
            _addNewDogViewModel.Name = "Dog";
            _addNewDogViewModel.Breed = "african";
            _addNewDogViewModel.Description = "Description";
            _addNewDogViewModel.Sex = "Male";
            _addNewDogViewModel.Price = 120;
            _addNewDogViewModel.FetchARandomImageCommand.Execute();

            _addNewDogViewModel.AddNewDogCommand.Execute();

            _mockNavigationService.Verify(x => x.NavigateAsync("AddNewDogPage/" + nameof(DogsListPage)), Times.Once());
        }

        [Fact]
        public void AddNewDogCommand_WhenAllDogAttributIsNotReallyHonored_ShouldStillNavigateToDogsList()
        {
            _addNewDogViewModel.Name = "Dog";
            _addNewDogViewModel.Breed = "african";
            _addNewDogViewModel.Price = 120;
            _addNewDogViewModel.FetchARandomImageCommand.Execute();

            _addNewDogViewModel.AddNewDogCommand.Execute();

            _mockNavigationService.Verify(x => x.NavigateAsync("AddNewDogPage/" + nameof(DogsListPage)), Times.Once());
        }

        [Fact]
        public void AddNewDogCommand_WhenAllDogAttributIsNotHonored_ShouldNotNavigateToDogsList()
        {
            //Arrange
            _addNewDogViewModel.Breed = "african";
            _addNewDogViewModel.Price = 120;

            //Act
            _addNewDogViewModel.AddNewDogCommand.Execute();
            //Assert
            _mockNavigationService.VerifyNoOtherCalls();
        }

        [Fact]
        public void AddNewDogCommand_WhenAllDogNameAttributIsNotHonored_AlertShouldBeSet()
        {
            _addNewDogViewModel.Breed = "african";
            _addNewDogViewModel.Price = 120;
            //Act
            _addNewDogViewModel.AddNewDogCommand.Execute();
            //Assert
            _mockPageDialogService.Verify(x => x.DisplayAlertAsync(UiText.ErrorExceptionThrowTitle, UiText.NameAndPriceShouldNotBeEmptyException, UiText.Okay));
        }

        [Fact]
        public void AddNewDogCommand_WhenAllDogPriceAttributIsNotHonored_AlertShouldBeSet()
        {
            _addNewDogViewModel.Name = "Dog";
            _addNewDogViewModel.Breed = "african";
            //Act
            _addNewDogViewModel.AddNewDogCommand.Execute();
            //Assert
            _mockPageDialogService.Verify(x => x.DisplayAlertAsync(UiText.ErrorExceptionThrowTitle, UiText.NameAndPriceShouldNotBeEmptyException, UiText.Okay));
        }

        [Fact]
        public void FetchARandomImageCommand_WhenAllDogAttributIsNotReallyHonored_ShouldStillNavigateToDogsList()
        {
            _addNewDogViewModel.Breed = "african";

            _addNewDogViewModel.FetchARandomImageCommand.Execute();

            _addNewDogViewModel.ImageUrl.Should().NotBeNull();        
        }

        [Fact]
        public void FetchARandomImageCommand_WhenAllDogAttributNotReallyHonored_ShouldStillNavigateToDogsList()
        {
            _addNewDogViewModel.Breed = "african";

            _addNewDogViewModel.FetchARandomImageCommand.Execute();
            string FIRST_IMAGE_RETURN = _addNewDogViewModel.ImageUrl;

            _addNewDogViewModel.FetchARandomImageCommand.Execute();
            string SECOND_IMAGE_RETURN = _addNewDogViewModel.ImageUrl;

            FIRST_IMAGE_RETURN.Should().NotContainEquivalentOf(SECOND_IMAGE_RETURN);
        }

    }
}
