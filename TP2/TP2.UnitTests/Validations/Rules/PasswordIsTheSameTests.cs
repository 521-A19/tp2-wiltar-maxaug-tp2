using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using TP2.Validation.Rules;
using Xunit;

namespace TP2.UnitTests.Validations.Rules
{
    public class PasswordIsTheSameTests
    {
        private readonly PasswordIsTheSame<string> _rule;

        public PasswordIsTheSameTests()
        {
            _rule = new PasswordIsTheSame<string>("123456789aA");
        }

        [Fact]
        public void Check_WhenStringIsNull_ShouldReturnFalse()
        {
            const string NULL_STRING = null;

            var isValid = _rule.Check(NULL_STRING);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("123456789cS")]
        [InlineData("12345gfC")]
        public void Check_WhenStringIsNotTheRightPassword_ShouldReturnFalse(string notValidEmailString)
        {
            var isValid = _rule.Check(notValidEmailString);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("123456789aA")]
        public void Check_WhenStringIsRightPassword_ShouldReturnTrue(string ValidEmailString)
        {
            var isValid = _rule.Check(ValidEmailString);

            isValid.Should().BeTrue();
        }
    }
}

