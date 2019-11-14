using Moq;
using Prism.Navigation;
using System;
using TP2.ViewModels;
using TP2.Views;
using Xunit;

namespace TP2.UnitTests
{
    public class DogsListViewModelTests
    {
        private MainPageViewModel _mainPageViewModel;
        private Mock<INavigationService> _mockNavigationService;
        public DogsListViewModelTests()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _mainPageViewModel = new MainPageViewModel(_mockNavigationService.Object);
        }

        [Fact]
        public void GoToDogsListCommand_ShouldChangePageToDogsList()
        {
            _mainPageViewModel.GoToDogsListCommand.Execute();

            _mockNavigationService.Verify(x => x.NavigateAsync("NavigationPage/" + nameof(DogsListPage)), Times.Once());
        }
    }
}
