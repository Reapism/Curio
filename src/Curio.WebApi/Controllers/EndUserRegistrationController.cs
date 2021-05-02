using System.Threading;
using System.Threading.Tasks;
using Curio.SharedKernel;
using Curio.WebApi.Exchanges.Home;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Curio.WebApi.Controllers
{
    public class EndUserRegistrationController : CurioControllerBase
    {
        public EndUserRegistrationController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> RegisterEndUser([FromForm] EndUserRegistrationRequest endUserRegistrationRequest, CancellationToken cancellationToken = default)
        {
            var result = await Mediator.Send(endUserRegistrationRequest, cancellationToken);
            return Ok(result);
        }
    }
}
