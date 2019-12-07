using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using TP2.Validation.Rules;

namespace UnitTest.Validations.Rules
{
    public class ContainAtLeastOneNumericCharacterTests
    {
        private readonly ContainAtLeastOneNumericCharacter<string> _rule;

        public ContainAtLeastOneNumericCharacterTests()
        {
            _rule = new ContainAtLeastOneNumericCharacter<string>();


        }

        [Fact]
        public void Check_WhenStringIsNull_ShouldReturnFalse()
        {
            const string NULL_STRING = null;

            var isValid = _rule.Check(NULL_STRING);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("A")]
        [InlineData("AA    ")]
        public void Check_WhenStringHasNoNumericCharacter_ShouldReturnFalse(string noNumericCharacterString)
        {
            var isValid = _rule.Check(noNumericCharacterString);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("3")]
        [InlineData("456    ")]
        public void Check_WhenStringHasNumericCharacter_ShouldReturnTrue(string NumericCharacterString)
        {
            var isValid = _rule.Check(NumericCharacterString);

            Assert.True(isValid);
        }
    }
}
