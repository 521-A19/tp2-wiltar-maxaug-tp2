using Prism.Commands;
using Prism.Mvvm;
using TP2.Services;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TP2.Externalization;
using TP2.Validation;
using TP2.Validation.Rules;
using TP2.Views;

namespace TP2.ViewModels
{
    public class RegisterPageViewModel : ViewModelBase
    {
        private IRegistrationService _registrationService;
        public DelegateCommand NavigateToMainPageCommand => new DelegateCommand(ExecuteNavigateToMainPageCommand);

        private ValidatableObject<string> _userName;
        private ValidatableObject<string> _password;
        private ValidatableObject<string> _secondPassword;
        public ValidatableObject<string> UserName
        {
            get => _userName;

            // Non nécessaire. Les "raises" sont gérés dans le ValidatableObject. 
            //set
            //{
            //    _userName = value;
            //    RaisePropertyChanged();
            //}
        }
        public ValidatableObject<string> Password
        {
            get => _password;
        }

        public ValidatableObject<string> SecondPassword
        {
            get => _secondPassword;
        }

        public RegisterPageViewModel(INavigationService navigationService, IRegistrationService registrationService)
            : base(navigationService)
        {
            Title = "Register Page";
            _registrationService = registrationService;
            _userName = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();
            _secondPassword = new ValidatableObject<string>();
            AddValidationRulesToValidatable();
        }

        public DelegateCommand ValidateUserNameCommand => new DelegateCommand(ValidateUserName);
        public DelegateCommand ValidatePasswordCommand => new DelegateCommand(ValidatePassword);
        public DelegateCommand ValidateSecondPasswordCommand => new DelegateCommand(ValidateSecondPassword);

        private void ValidateUserName()
        {
            _userName.Validate();
        }

        private void ValidatePassword()
        {
            _password.Validate();
        }

        private void ValidateSecondPassword()
        {
            _secondPassword.Validate();
        }
        private void AddValidationRulesToValidatable()
        {
            var passwordIsTheSame = new PasswordIsTheSame<string>(Password.Value)
            {
                ErrorMessage = UiText.SecondPasswordIsTheSameOfTheFirst
            };

            var containAtLeastOneLowercaseLetter = new ContainAtLeastOneLowercaseLetter<string>
            {
                ErrorMessage = UiText.LowercaseRequired
            };
            var containAtLeastOneNumericCharacter = new ContainAtLeastOneNumericCharacter<string>
            {
                ErrorMessage = UiText.NumericCharacterRequired
            };
            var containAtLeastOneUpercaseLetter = new ContainAtLeastOneUpercaseLetter<string>
            {
                ErrorMessage = UiText.UppercaseRequired
            };
            var containMoreThanTenCharacters = new ContainMoreThanTenCharacters<string>
            {
                ErrorMessage = UiText.MoreThanTenCharactersRequired
            };
            var isValidEmailAddress = new IsValidEmailAddress<string>
            {
                ErrorMessage = UiText.ValidEmailRequired
            };
            _userName.AddValidationRule(isValidEmailAddress);
            _password.AddValidationRule(containAtLeastOneLowercaseLetter);
            _password.AddValidationRule(containAtLeastOneNumericCharacter);
            _password.AddValidationRule(containAtLeastOneUpercaseLetter);
            _password.AddValidationRule(containMoreThanTenCharacters);
            _secondPassword.AddValidationRule(passwordIsTheSame);
        }

        private void ExecuteNavigateToMainPageCommand()
        {
            ValidateUserName();
            ValidatePassword();
            //ValidateSecondPassword();
            if (Password.Errors.Count + UserName.Errors.Count == 0)
            {
                _registrationService.RegisterUser(_userName.Value, _password.Value);
                NavigationService.NavigateAsync("/" + nameof(MainPage));

            }

        }
    }
}
