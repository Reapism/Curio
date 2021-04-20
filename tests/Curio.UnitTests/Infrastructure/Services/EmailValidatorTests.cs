using Curio.Infrastructure.Services;
using FluentAssertions;
using NUnit.Framework;

namespace Curio.UnitTests.Infrastructure.Services
{
    [TestFixture]
    public class EmailValidatorTests
    {
        [TestCase(null, null)]
        [TestCase("", "@")]
        [TestCase("", "")]
        [TestCase("hi", "hi")]
        [TestCase("hi", "hi@email.com")]
        [TestCase("hi", "hi@hi.com")]
        [TestCase("hi", "hi@")]
        [TestCase("", "@@")]
        [TestCase("", "@.@")]
        public void GetEmailNameFromEmail_ShouldReturnEmailName(string expected, string actual)
        {
            var validator = new EmailValidator();
            validator.GetEmailName(actual).Should().BeSameAs(expected);
        }
    }
}
