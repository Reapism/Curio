using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Curio.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurioControllerBase : ControllerBase
    {
        public CurioControllerBase(IMediator mediator)
        {
            Mediator = mediator;
        }

        protected IMediator Mediator { get; }
    }
}
