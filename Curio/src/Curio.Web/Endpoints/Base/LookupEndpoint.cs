using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Curio.Web.Endpoints.Base
{
    [ApiController]
    public abstract class LookupEndpoint<TRequest, TResponse> : ControllerBase
    {
        [HttpGet]
        public abstract ActionResult<TResponse> HandleLookupAsync();

        [HttpGet]
        public abstract ActionResult<TResponse> HandleLookupAsync(TRequest request);
    }

    [ApiController]
    public abstract class LookupEndpointAsync<TRequest, TResponse> : ControllerBase
    {
        [HttpGet]
        public abstract Task<ActionResult<TResponse>> HandleLookupAsync(
            CancellationToken cancellationToken = default);

        [HttpGet]
        public abstract Task<ActionResult<TResponse>> HandleLookupAsync(
            TRequest request,
            CancellationToken cancellationToken = default);
    }
}
