using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using TP2.Validation.Rules;


namespace UnitTest.Validations.Rules
{
    public class ContainAtLeastOneUpercaseLetterTests
    {
        private readonly ContainAtLeastOneUpercaseLetter<string> _rule;

        public ContainAtLeastOneUpercaseLetterTests()
        {
            _rule = new ContainAtLeastOneUpercaseLetter<string>();


        }

        [Fact]
        public void Check_WhenStringIsNull_ShouldReturnFalse()
        {
            const string NULL_STRING = null;

            var isValid = _rule.Check(NULL_STRING);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("aa   ")]
        public void Check_WhenStringHasNoUppercase_ShouldReturnFalse(string noUppercaseString)
        {
            var isValid = _rule.Check(noUppercaseString);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("A")]
        [InlineData("AA   ")]
        public void Check_WhenStringHasUppercase_ShouldReturnTrue(string UppercaseString)
        {
            var isValid = _rule.Check(UppercaseString);

            Assert.True(isValid);
        }
    }
}
