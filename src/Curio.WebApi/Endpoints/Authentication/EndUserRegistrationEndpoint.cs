using System.Threading;
using System.Threading.Tasks;
using Curio.Api.Endpoints.Base;
using Curio.WebApi.Exchanges.Home;
using Microsoft.AspNetCore.Mvc;

namespace Curio.WebApi.Endpoints.Authentication
{
    public class EndUserRegistrationEndpoint : EndpointBaseAsyncBuilder
        .WithRequest<EndUserRegistrationRequest>
        .WithResponse<RegistrationResponse>
    {
        private readonly IUserRegistrationService userRegistrationService;

        public EndUserRegistrationEndpoint(IUserRegistrationService userRegistrationService, Mediator)
        {
            this.userRegistrationService = userRegistrationService;
        }

        [HttpPost]
        public override Task<ActionResult<RegistrationResponse>> HandleAsync(EndUserRegistrationRequest request, CancellationToken cancellationToken = default)
        {

        }
    }
}
