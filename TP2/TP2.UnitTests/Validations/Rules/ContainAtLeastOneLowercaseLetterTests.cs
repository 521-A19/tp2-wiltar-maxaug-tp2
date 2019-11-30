using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using TP2.Validation.Rules;
using Xunit;

namespace UnitTest.Validations.Rules
{
    public class ContainAtLeastOneLowercaseLetterTests
    {
        private readonly ContainAtLeastOneLowercaseLetter<string> _rule;

        public ContainAtLeastOneLowercaseLetterTests()
        {
            _rule = new ContainAtLeastOneLowercaseLetter<string>();


        }

        [Fact]
        public void Check_WhenStringIsNull_ShouldReturnFalse()
        {
            const string NULL_STRING = null;

            var isValid = _rule.Check(NULL_STRING);

            isValid.Should().BeFalse();
        }

        [Theory]
        [InlineData("A")]
        [InlineData("AA    ")]
        public void Check_WhenStringHasNoLowercase_ShouldReturnFalse(string noLowercaseString)
        {
            var isValid = _rule.Check(noLowercaseString);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("aa   ")]
        public void Check_WhenStringHasLowercase_ShouldReturnTrue(string LowercaseString)
        {
            var isValid = _rule.Check(LowercaseString);

            Assert.True(isValid);
        }
    }
}
