using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Curio.Api.Endpoints.Base
{
    [ApiController]
    public abstract class LookupEndpoint<TRequest, TResponse> : ControllerBase
    {
        [HttpGet]
        public abstract ActionResult<TResponse> LookupAsync();

        [HttpGet]
        public abstract ActionResult<TResponse> LookupAsync(TRequest request);
    }

    [ApiController]
    public abstract class LookupEndpointAsync<TRequest, TResponse> : ControllerBase
    {
        [HttpGet]
        public abstract Task<ActionResult<TResponse>> LookupAsync(
            CancellationToken cancellationToken = default);

        [HttpGet]
        public abstract Task<ActionResult<TResponse>> LookupAsync(
            TRequest request,
            CancellationToken cancellationToken = default);
    }
}
