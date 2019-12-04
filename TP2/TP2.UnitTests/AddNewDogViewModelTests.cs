using Bogus;
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
using TP2.UnitTests.Fixture;
using Xunit;

namespace TP2.UnitTests
{
    public class AddNewDogViewModelTests : BaseFixture
    {
        AddNewDogViewModel _addNewDogViewModel;
        private Mock<INavigationService> _mockNavigationService;
        private Mock<IPageDialogService> _mockPageDialogService;
        private Mock<IRepository<Dog>> _mockDogRepository;
        private Mock<IRepository<User>> _mockUserRepository;
        private Mock<IAuthenticationService> _mockAuthentification;
        private IDogApiService _dogApiService;
        private List<User> _userList;
        private List<Dog> _dogList;
        private Fixture.Fixture _fixture = new Fixture.Fixture();

        public AddNewDogViewModelTests()
        {
            _userList = _fixture.BuildUsersList();
            _dogList = _fixture.BuildDogsList();
            _mockNavigationService = new Mock<INavigationService>();
            _mockPageDialogService = new Mock<IPageDialogService>();
            _mockDogRepository = new Mock<IRepository<Dog>>();
            _mockUserRepository = new Mock<IRepository<User>>();
            _dogApiService = new DogApiService();
            _mockAuthentification = new Mock<IAuthenticationService>();
            _addNewDogViewModel = new AddNewDogViewModel(_mockNavigationService.Object, _dogApiService, _mockDogRepository.Object, _mockUserRepository.Object, _mockPageDialogService.Object, _mockAuthentification.Object);
        }

        [Fact]
        public void AddNewDogCommand_WhenAllDogAttributIsHonored_ShouldNavigateToDogsList()
        {
            //Arrange
            _mockAuthentification
              .Setup(a => a.AuthenticatedUser)
              .Returns(_userList[0]);

            _mockDogRepository
                .Setup(n => n.GetAll())
                .Returns(_dogList);

            _addNewDogViewModel.Name = "Dog";
            _addNewDogViewModel.Breed = "african";
            _addNewDogViewModel.Description = "Description";
            _addNewDogViewModel.Sex = "Male";
            _addNewDogViewModel.Price = 120;
            _addNewDogViewModel.FetchARandomImageCommand.Execute();

            //Act
            _addNewDogViewModel.AddNewDogCommand.Execute();

            //Assert
            _mockNavigationService.Verify(x => x.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(DogsListPage)), Times.Once());
        }

        [Fact]
        public void AddNewDogCommand_WhenAllDogAttributIsNotReallyHonored_ShouldStillNavigateToDogsList()
        {
            _mockAuthentification
              .Setup(a => a.AuthenticatedUser)
              .Returns(_userList[0]); ;

            _mockDogRepository
               .Setup(n => n.GetAll())
               .Returns(_dogList);

            _addNewDogViewModel.Name = "Dog";
            _addNewDogViewModel.Breed = "african";
            _addNewDogViewModel.Price = 120;
            _addNewDogViewModel.FetchARandomImageCommand.Execute();

            _addNewDogViewModel.AddNewDogCommand.Execute();

            _mockNavigationService.Verify(x => x.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(DogsListPage)), Times.Once());
        }

        [Fact]
        public void AddNewDogCommand_ShouldCallUpdateFromUserRepository()
        {
            //Arrange
            _mockAuthentification
              .Setup(a => a.AuthenticatedUser)
              .Returns(_userList[0]); ;
            _mockDogRepository
               .Setup(n => n.GetAll())
               .Returns(_dogList);
            _addNewDogViewModel.Name = "Dog";
            _addNewDogViewModel.Breed = "african";
            _addNewDogViewModel.Price = 120;
            _addNewDogViewModel.FetchARandomImageCommand.Execute();

            //Act
            _addNewDogViewModel.AddNewDogCommand.Execute();

            //Assert
            _mockUserRepository.Verify(x => x.Update(It.IsAny<User>()), Times.Once());
        }

        [Fact]
        public void AddNewDogCommand_WhenExceptionIsThrow_ShouldDisplayAlert()
        {
            //Arrange
            _mockAuthentification
               .Setup(a => a.AuthenticatedUser)
               .Returns(_userList[0]);
            _addNewDogViewModel.Breed = "african";
            _addNewDogViewModel.Price = 120;
            _mockNavigationService
                .Setup(a => a.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(DogsListPage)))
                .Throws<Exception>();

            //Act
            _addNewDogViewModel.AddNewDogCommand.Execute();

            //Assert
            _mockPageDialogService.Verify(x => x.DisplayAlertAsync(UiText.ErrorExceptionThrowTitle, UiText.ErrorExceptionThrowMessage, UiText.Okay));
        }

        [Fact]
        public void FetchARandomImageCommand_WhenAllDogAttributIsNotReallyHonored_ShouldStillNavigateToDogsList()
        {
            _mockAuthentification
              .Setup(a => a.AuthenticatedUser)
              .Returns(_userList[0]);
            _addNewDogViewModel.Breed = "african";

            _addNewDogViewModel.FetchARandomImageCommand.Execute();

            _addNewDogViewModel.ImageUrl.Should().NotBeNull();        
        }

        [Fact]
        public void FetchARandomImageCommand_WhenAllDogAttributNotReallyHonored_ShouldStillNavigateToDogsList()
        {
            //Arrange
            _mockAuthentification
              .Setup(a => a.AuthenticatedUser)
              .Returns(_userList[0]); ;
            _addNewDogViewModel.Breed = "african";
            _addNewDogViewModel.FetchARandomImageCommand.Execute();
            string FIRST_IMAGE_RETURN = _addNewDogViewModel.ImageUrl;

            //Act
            _addNewDogViewModel.FetchARandomImageCommand.Execute();
            string SECOND_IMAGE_RETURN = _addNewDogViewModel.ImageUrl;

            //Assert
            FIRST_IMAGE_RETURN.Should().NotContainEquivalentOf(SECOND_IMAGE_RETURN);
        }
    }
}
