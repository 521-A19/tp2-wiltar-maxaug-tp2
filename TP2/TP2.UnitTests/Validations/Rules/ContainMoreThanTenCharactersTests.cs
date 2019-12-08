using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using TP2.Validation.Rules;

namespace UnitTest.Validations.Rules
{
    public class ContainMoreThanTenCharactersTests
    {
        private readonly ContainMoreThanTenCharacters<string> _rule;

        public ContainMoreThanTenCharactersTests()
        {
            _rule = new ContainMoreThanTenCharacters<string>();


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
        public void Check_WhenStringHasnotMoreThanTenCharacters_ShouldReturnFalse(string lessThanTenCharactersString)
        {
            var isValid = _rule.Check(lessThanTenCharactersString);

            Assert.False(isValid);
        }


        [Theory]
        [InlineData("abcdefghijk")]
        [InlineData("abcdefghijklm   ")]
        public void Check_WhenStringHasMoreThanTenCharacters_ShouldReturnTrue(string MoreThanTenCharactersString)
        {
            var isValid = _rule.Check(MoreThanTenCharactersString);

            Assert.True(isValid);
        }
    }
}
