using TP2.Validation;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xunit;
using FluentAssertions;

namespace UnitTest.Validations
{
    public class ValidatableObjectTests
    {
        private readonly ValidatableObject<string> _validatableObject;
        private Mock<IValidationRule<string>> _mockValidationRule;
        private IList<string> _eventRaisedPropertyNames = new List<string>();

        public ValidatableObjectTests()
        {
            _mockValidationRule = new Mock<IValidationRule<string>>();
            _validatableObject = new ValidatableObject<string>();

        }

        [Fact]
        public void Validate_WhenNoRuleHasBeenAdded_ThenValidatableObjectShouldBeValid()
        {
            _validatableObject.Validate();

            _validatableObject.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_WhenNoRuleHasBeenAdded_ThenValidatableObjectShouldContainNoError()
        {
            _validatableObject.Validate();

            _validatableObject.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Validate_WhenRuleIsHonored_ThenValidatableObjectShouldBeValid()
        {
            _validatableObject.AddValidationRule(_mockValidationRule.Object);
            _mockValidationRule
           .Setup(v => v.Check("123456789aA"))
           .Returns(true);

            _validatableObject.Validate();

            _validatableObject.IsValid.Should().BeTrue();
        }


        [Fact]
        public void Validate_WhenRuleIsHonored_ThenValidatableObjectShouldContainsNoError()
        {
            _validatableObject.AddValidationRule(_mockValidationRule.Object);
            _mockValidationRule
          .Setup(a => a.Check(""))
          .Returns(true);
           /*_mockValidationRule
          .Setup(n => n.ErrorMessage)
          .Returns("");*/

            _validatableObject.Validate();

            _validatableObject.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Validate_WhenRuleIsNotHonored_ThenValidatableObjectShouldNotBeValid()
        {
            _validatableObject.AddValidationRule(_mockValidationRule.Object);
            _mockValidationRule
          .Setup(a => a.Check(""))
          .Returns(false);

            _validatableObject.Validate();

            _validatableObject.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_WhenRuleIsNotHonored_ThenRuleErrorShouldBeAddedToValidatableObjectErrors()
        {
            _validatableObject.AddValidationRule(_mockValidationRule.Object);
            _mockValidationRule
          .Setup(a => a.Check(""))
          .Returns(false);

            _validatableObject.Validate();

            _validatableObject.Errors.Should().ContainSingle();
        }

        [Fact]
        public void Validate_WhenRulesAreNotHonored_ThenValidatableObjectShouldNotBeValid()
        {
            _validatableObject.AddValidationRule(_mockValidationRule.Object);
            _validatableObject.AddValidationRule(_mockValidationRule.Object);
            _mockValidationRule
          .Setup(a => a.Check(""))
          .Returns(false);

            _validatableObject.Validate();

            _validatableObject.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_WhenRulesAreNotHonored_ThenRulesErrorShouldBeAddedToValidatableObjectErrors()
        {
            const int NUMBER_OF_RULES_ADDED = 2;

            _validatableObject.AddValidationRule(_mockValidationRule.Object);
            _validatableObject.AddValidationRule(_mockValidationRule.Object);
            _mockValidationRule
          .Setup(a => a.Check(""))
          .Returns(false);

            _validatableObject.Validate();

            NUMBER_OF_RULES_ADDED.Should().Equals(_validatableObject.Errors.Count);
        }

        [Fact]
        public void Validate_WhenCall_ShouldRaisePropertyChangedEventForErrorsAnIsValid()
        {
            _validatableObject.PropertyChanged += RaiseProperty;
            _validatableObject.AddValidationRule(_mockValidationRule.Object);

            _validatableObject.Validate();

            Assert.Contains<string>(nameof(_validatableObject.Errors), _eventRaisedPropertyNames);
            Assert.Contains<string>(nameof(_validatableObject.IsValid), _eventRaisedPropertyNames);
        }

        [Fact]
        public void Value_WhenSetToNewValue_ShouldRaisePropertyChangedEvent()
        {
            _validatableObject.PropertyChanged += RaiseProperty;

            _validatableObject.Value = "new value";

            Assert.Contains<string>(nameof(_validatableObject.Value), _eventRaisedPropertyNames);
        }

        private void RaiseProperty(object sender, PropertyChangedEventArgs e)
        {
            _eventRaisedPropertyNames.Add(e.PropertyName);
        }
    }
}
