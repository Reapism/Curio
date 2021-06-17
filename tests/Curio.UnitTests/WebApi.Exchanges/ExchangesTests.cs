using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Curio.WebApi.Exchanges.Identity;
using FluentAssertions;
using NUnit.Framework;

namespace Curio.UnitTests.WebApi.Exchanges
{
    [TestFixture]
    public class ExchangesTests
    {
        [Test]
        public void ValidationResponseIsFullyInitialized()
        {
            var loginResponse = new LoginResponse();
            loginResponse.ReasonByErrorMapping.Should().NotBeNull().And.BeEmpty();
        }

        [Test]
        public void ValidationResponseByDefaultIsNull()
        {
            var loginResponse = default(LoginResponse);
            loginResponse.ReasonByErrorMapping.Should().BeNull();
        }
    }
}
