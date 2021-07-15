using Curio.WebApi.Exchanges.Identity;
using FluentAssertions;
using Xunit;

namespace Curio.UnitTests.WebApi.Exchanges
{
    public class ExchangesTests
    {
        [Fact]
        public void ValidationResponseIsFullyInitialized()
        {
            var loginResponse = new LoginResponse();
            loginResponse.ReasonByErrorMapping.Should().NotBeNull().And.BeEmpty();
        }

        [Fact]
        public void ValidationResponseByDefaultIsNull()
        {
            var loginResponse = default(LoginResponse);
            loginResponse?.ReasonByErrorMapping.Should().BeNull();
        }
    }
}
