using System.Threading;
using System.Threading.Tasks;
using Curio.Api.Endpoints.Base;
using Curio.WebApi.Exchanges.Home;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Curio.WebApi.Endpoints.Authentication
{
    public class EndUserRegistrationEndpoint : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IUserRegistrationService userRegistrationService;

        public EndUserRegistrationEndpoint(IMediator mediator, IUserRegistrationService userRegistrationService)
        {
            this.userRegistrationService = userRegistrationService;
            this.mediator = mediator;
        }
    }
}
