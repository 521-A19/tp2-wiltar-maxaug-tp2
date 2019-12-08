using Moq;
using Prism.Navigation;
using Prism.Services;
using TP2.Externalization;
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
        public void ConfirmRegistrationCommand_ShouldNavigateToMainPage()
        {
            _mockRegistrationService
               .Setup(a => a.IsLoginAlreadyRegistered)
              .Returns(false);
            _registerPageViewModel.UserName.Value = "email@test.com";
            _registerPageViewModel.Password.Value = "123456789aA";
            _registerPageViewModel.SecondPassword.Value = "123456789aA";

            _registerPageViewModel.ConfirmRegistrationCommand.Execute();

            _mockNavigationService.Verify(x => x.NavigateAsync("/CustomMasterDetailPage/NavigationPage/" + nameof(MainPage)), Times.Once());
            _mockNavigationService.VerifyNoOtherCalls();
        }

        [Fact]
        public void LoginAlreadyRegistered_ConfirmRegistrationCommand_ShouldDisplayAlertMessage()
        {
            _mockRegistrationService
                .Setup(a => a.IsLoginAlreadyRegistered)
               .Returns(true); 
            _registerPageViewModel.UserName.Value = "email@test.com";
            _registerPageViewModel.Password.Value = "123456789aA";
            _registerPageViewModel.SecondPassword.Value = "123456789aA";

            _registerPageViewModel.ConfirmRegistrationCommand.Execute();

            _mockPageDialogService.Verify(x => x.DisplayAlertAsync(UiText.USER_REGISTER_ALERT, UiText.LOGIN_IS_ALREADY_REGISTERED, UiText.OKAY_CHANGE_NAME));
        }

        [Fact]
        public void InvalidEmail_ValidateUserNameCommand_UserNameErrorsShouldBeAdded()
        {
            _registerPageViewModel.UserName.Value = "invalidEmail";

            _registerPageViewModel.ValidateUserNameCommand.Execute();

            Assert.Equal(UiText.ValidEmailRequired,_registerPageViewModel.UserName.Errors[0]);
            Assert.Single(_registerPageViewModel.UserName.Errors);
        }


        [Fact]
        public void ValidEmail_ValidateUserNameCommand_UserNameErrorsShouldBeEmpty()
        {
            _registerPageViewModel.UserName.Value = "valid@email.com";

            _registerPageViewModel.ValidateUserNameCommand.Execute();

            Assert.Empty(_registerPageViewModel.UserName.Errors);
        }

        [Fact]
        public void InvalidPasswordWithoutLowercaseCharacter_ValidatePasswordCommand_PasswordErrorsShouldBeAdded()
        {
            _registerPageViewModel.Password.Value = "A1";

            _registerPageViewModel.ValidatePasswordCommand.Execute();

            Assert.Equal(UiText.LowercaseRequired, _registerPageViewModel.Password.Errors[0]);
        }

        [Fact]
        public void InvalidPasswordWithoutUppercaseCharacter_ValidatePasswordCommand_PasswordErrorsShouldBeAdded()
        {
            _registerPageViewModel.Password.Value = "a1";

            _registerPageViewModel.ValidatePasswordCommand.Execute();

            Assert.Equal(UiText.UppercaseRequired, _registerPageViewModel.Password.Errors[0]);
        }


        [Fact]
        public void InvalidPasswordWithoutNumericCharacter_ValidatePasswordCommand_PasswordErrorsShouldBeAdded()
        {
            _registerPageViewModel.Password.Value = "Aaaa";

            _registerPageViewModel.ValidatePasswordCommand.Execute();

            Assert.Equal(UiText.NumericCharacterRequired, _registerPageViewModel.Password.Errors[0]);
        }

        [Fact]
        public void InvalidPasswordWithoutMoreThanTenCharacters_ValidatePasswordCommand_PasswordErrorsShouldBeAdded()
        {
            _registerPageViewModel.Password.Value = "Abc123";

            _registerPageViewModel.ValidatePasswordCommand.Execute();

            Assert.Equal(UiText.MoreThanTenCharactersRequired, _registerPageViewModel.Password.Errors[0]);
        }

        [Fact]
        public void ValidPassword_ValidatePasswordCommand_PasswordErrorsShouldBeEmpty()
        {
            _registerPageViewModel.Password.Value = "1ValidPassword";

            _registerPageViewModel.ValidatePasswordCommand.Execute();

            Assert.Empty(_registerPageViewModel.Password.Errors);
        }

        [Fact]
        public void InvalidSecondPassword_ConfirmRegistrationCommand_SecondPasswordErrorsShouldBeAdded()
        {
            _registerPageViewModel.Password.Value = "1ValidPassword";
            _registerPageViewModel.SecondPassword.Value = "invalidSecondPassword";

            _registerPageViewModel.ConfirmRegistrationCommand.Execute();

            Assert.Equal(UiText.SecondPasswordIsTheSameOfTheFirst, _registerPageViewModel.SecondPassword.Errors[0]);
        }

        [Fact]
        public void ValidSecondPassword_ConfirmRegistrationCommand_SecondPasswordErrorsShouldBeEmpty()
        {
            _registerPageViewModel.Password.Value = "1ValidPassword";
            _registerPageViewModel.SecondPassword.Value = "1ValidPassword";

            _registerPageViewModel.ConfirmRegistrationCommand.Execute();

            Assert.Empty(_registerPageViewModel.SecondPassword.Errors);
        }
    }
}
