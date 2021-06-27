using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Curio.WebApi.Endpoints.Base
{
    [ApiController]
    public abstract class LookupEndpoint<TRequest, TResponse> : ControllerBase
    {
        [HttpGet]
        public abstract ActionResult<TResponse> OnLookup(TRequest request);
    }

    [ApiController]
    public abstract class LookupEndpointAsync<TRequest, TResponse> : ControllerBase
    {
        [HttpGet]
        public abstract Task<ActionResult<TResponse>> OnLookupAsync(
            TRequest request,
            CancellationToken cancellationToken = default);
    }
}
