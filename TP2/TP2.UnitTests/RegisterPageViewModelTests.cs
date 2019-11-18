using Moq;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using TP2.Services;
using TP2.ViewModels;
using TP2.Views;
using Xunit;

namespace TP2.UnitTests
{
    public class RegisterPageViewModelTests
    {
        private RegisterPageViewModel _registerPageViewModel;
        private Mock<INavigationService> _mockNavigationService;
        private Mock<IRegistrationService> _mockRegistrationService;
        public RegisterPageViewModelTests()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _mockRegistrationService = new Mock<IRegistrationService>();
            _registerPageViewModel = new RegisterPageViewModel(_mockNavigationService.Object, _mockRegistrationService.Object );
        }

        [Fact]
        public void NavigateToMainPageCommand_ShouldNavigateToMainPage()
        {
            _registerPageViewModel.UserName.Value = "email@test.com";
            _registerPageViewModel.Password.Value = "123456789aA";
            _registerPageViewModel.NavigateToMainPageCommand.Execute();

            _mockNavigationService.Verify(x => x.NavigateAsync("/" + nameof(MainPage)), Times.Once());
        }
    }
}
