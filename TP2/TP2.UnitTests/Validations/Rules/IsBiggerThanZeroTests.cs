using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using TP2.Validation.Rules;
using Xunit;

namespace TP2.UnitTests.Validations.Rules
{
    public class IsBiggerThanZeroTests
    {
        private readonly IsBiggerThanZero<string> _rule;

        public IsBiggerThanZeroTests()
        {
            _rule = new IsBiggerThanZero<string>();


        }

        [Fact]
        public void Check_WhenStringIsNull_ShouldReturnFalse()
        {
            const string NULL_STRING = null;

            var isValid = _rule.Check(NULL_STRING);

            isValid.Should().BeFalse();
        }

        [Theory]
        [InlineData(2)]
        [InlineData(124)]
        public void Check_WhenFloatIsBiggerThanZero_ShouldReturnTrue(float priceBiggerThanZero)
        {
            var isValid = _rule.Check(priceBiggerThanZero.ToString());

            isValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-1234)]
        public void Check_WhenFloatIsNotBiggerThanZero_ShouldReturnFalse(float priceNotBiggerThanZero)
        {
            var isValid = _rule.Check(priceNotBiggerThanZero.ToString());

            isValid.Should().BeFalse();
        }

    }
}
