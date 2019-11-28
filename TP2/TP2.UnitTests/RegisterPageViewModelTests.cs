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
    public class RegisterPageViewModelTests
    {
        private RegisterPageViewModel _registerPageViewModel;
        private Mock<INavigationService> _mockNavigationService;
        private Mock<IRegistrationService> _mockRegistrationService;
        private Mock<IPageDialogService> _mockPageDialogService;
        public RegisterPageViewModelTests()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _mockRegistrationService = new Mock<IRegistrationService>();
            _mockPageDialogService = new Mock<IPageDialogService>();
            _registerPageViewModel = new RegisterPageViewModel(_mockNavigationService.Object, _mockRegistrationService.Object, _mockPageDialogService.Object);
        }

        [Fact]
        public void NavigateToMainPageCommand_WhenThereAreNoErrorsMessages_ShouldNavigateToMainPage()
        {
            _mockRegistrationService
               .Setup(a => a.IsUserRegistered)
              .Returns(false);

            _registerPageViewModel.UserName.Value = "email@test.com";
            _registerPageViewModel.Password.Value = "123456789aA";
            _registerPageViewModel.SecondPassword.Value = "123456789aA";
            _registerPageViewModel.NavigateToMainPageCommand.Execute();

            _mockNavigationService.Verify(x => x.NavigateAsync("/" + nameof(MainPage)), Times.Once());
        }

        [Fact]
        public void NavigateToMainPageCommand_WhenThereAreErrorsMessages_ShouldNotNavigateToMainPage()
        {
            _registerPageViewModel.UserName.Value = "email@test.com";
            _registerPageViewModel.Password.Value = "123456789";
            _registerPageViewModel.SecondPassword.Value = "123456789aA";
            _registerPageViewModel.NavigateToMainPageCommand.Execute();

            _mockNavigationService.VerifyNoOtherCalls();
        }

        [Fact]
        public void NavigateToMainPageCommand_WhenThereAreNoErrorsMessagesButUserIsAlreadyRegisted_ShouldSetAnAlert()
        {
            _mockRegistrationService
                .Setup(a => a.IsUserRegistered)
               .Returns(true); 

            _registerPageViewModel.UserName.Value = "email@test.com";
            _registerPageViewModel.Password.Value = "123456789aA";
            _registerPageViewModel.SecondPassword.Value = "123456789aA";
            _registerPageViewModel.NavigateToMainPageCommand.Execute();

            

            _mockPageDialogService.Verify(x => x.DisplayAlertAsync(UiText.USER_REGISTER_ALERT, UiText.USER_IS_ALREADY_REGISTED, UiText.OKAY_CHANGE_NAME));
        }
    }
    
}
