using Moq;
using Prism.Navigation;
using Prism.Services;
using System;
using TP2.Services;
using TP2.ViewModels;
using TP2.Views;
using Xunit;

namespace TP2.UnitTests
{
    public class DogsListViewModelTests
    {
        private MainPageViewModel _mainPageViewModel;
        private Mock<INavigationService> _mockNavigationService;
        private Mock<IPageDialogService> _mockPageDialogService;
        private Mock<IAuthenticationService> _mockAuthenticationService;
        public DogsListViewModelTests()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _mockPageDialogService = new Mock<IPageDialogService>();
            _mockAuthenticationService = new Mock<IAuthenticationService>();
            _mainPageViewModel = new MainPageViewModel(_mockNavigationService.Object, _mockAuthenticationService.Object, _mockPageDialogService.Object);
        }

        [Fact]
        public void GoToDogsListCommand_ShouldChangePageToDogsList()
        {
            _mainPageViewModel.GoToDogsListCommand.Execute();

            _mockNavigationService.Verify(x => x.NavigateAsync("NavigationPage/" + nameof(DogsListPage)), Times.Once());
        }
    }
}
