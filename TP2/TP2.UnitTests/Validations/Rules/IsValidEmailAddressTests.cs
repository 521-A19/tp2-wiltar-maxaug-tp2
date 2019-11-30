using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using TP2.Validation.Rules;
using FluentAssertions;

namespace UnitTest.Validations.Rules
{
    public class IsValidEmailAddressTests
    {
        private readonly IsValidEmailAddress<string> _rule;

        public IsValidEmailAddressTests()
        {
            _rule = new IsValidEmailAddress<string>();
        }

        [Fact]
        public void Check_WhenStringIsNull_ShouldReturnFalse()
        {
            const string NULL_STRING = null;

            var isValid = _rule.Check(NULL_STRING);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("willi")]
        [InlineData("williams    ")]
        public void Check_WhenStringIsNotValidEmail_ShouldReturnFalse(string notValidEmailString)
        {
            var isValid = _rule.Check(notValidEmailString);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("williams@hotmail.com")]
        [InlineData("williams@hotmails.com.ca")]
        public void Check_WhenStringIsValidEmail_ShouldReturnTrue(string ValidEmailString)
        {
            var isValid = _rule.Check(ValidEmailString);

            isValid.Should().BeTrue();
        }
    }
}
