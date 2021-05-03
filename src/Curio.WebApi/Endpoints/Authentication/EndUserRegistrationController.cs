using System.Threading;
using System.Threading.Tasks;
using Curio.SharedKernel;
using Curio.WebApi.Controllers;
using Curio.WebApi.Exchanges.Home;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Curio.WebApi.Endpoints.Authentication
{
    public class EndUserRegistrationController : CurioControllerBase
    {
        public EndUserRegistrationController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<RegistrationResponse>>> RegisterEndUser([FromForm] EndUserRegistrationRequest endUserRegistrationRequest, CancellationToken cancellationToken = default)
        {
            var result = await Mediator.Send(endUserRegistrationRequest, cancellationToken);
            return Ok(result); // TODO use new extension method that will help return this based on ApiResponse status code.
        }
    }
}
