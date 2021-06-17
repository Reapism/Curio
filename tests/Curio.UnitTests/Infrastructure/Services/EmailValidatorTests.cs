using Curio.Infrastructure.Services;
using FluentAssertions;
using Xunit;

namespace Curio.UnitTests.Infrastructure.Services
{
    public class EmailValidatorTests
    {
        [Theory]
        [InlineData("", null)]
        [InlineData("", "@")]
        [InlineData("", "")]
        [InlineData("hi", "hi")]
        [InlineData("hi", "hi@email.com")]
        [InlineData("hi", "hi@hi.com")]
        [InlineData("hi", "hi@")]
        [InlineData("", "@@")]
        [InlineData("", "@.@")]
        public void GetEmailNameShouldReturnEmailName(string expected, string actual)
        {
            var validator = new EmailValidator();
            validator.GetEmailName(actual).Should().Be(expected);
        }
    }
}
