using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using TP2.Validation.Rules;
using Xunit;

namespace TP2.UnitTests.Validations.Rules
{
    public class HaveANameTests
    {
        private readonly HaveAName<string> _rule;

        public HaveANameTests()
        {
            _rule = new HaveAName<string>();


        }

        [Fact]
        public void Check_WhenStringIsNull_ShouldReturnFalse()
        {
            const string NULL_STRING = null;

            var isValid = _rule.Check(NULL_STRING);

            isValid.Should().BeFalse();
        }

        [Theory]
        [InlineData("olla")]
        [InlineData("Auli")]
        public void Check_WhenStringHasCharacters_ShouldReturnTrue(string Name)
        {
            var isValid = _rule.Check(Name);

            isValid.Should().BeTrue();
        }
    }
}
