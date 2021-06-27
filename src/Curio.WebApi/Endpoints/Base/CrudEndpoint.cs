using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Curio.WebApi.Endpoints.Base
{
    [ApiController]
    public abstract class CrudEndpoint<TRequest, TResponse> : ControllerBase
    {
        [HttpPost]
        public abstract ActionResult<TResponse> OnCreate(TRequest request);

        [HttpGet]
        public abstract ActionResult<TResponse> OnRead(TRequest request);

        [HttpPut]
        public abstract ActionResult<TResponse> OnUpdate(TRequest request);

        [HttpDelete]
        public abstract ActionResult<TResponse> OnDelete(TRequest request);
    }

    [ApiController]
    public abstract class CrudEndpointAsync<TRequest, TResponse> : ControllerBase
    {
        [HttpPost]
        public abstract Task<ActionResult<TResponse>> OnCreateAsync(
            TRequest request,
            CancellationToken cancellationToken = default);

        [HttpGet]
        public abstract Task<ActionResult<TResponse>> OnReadAsync(
            TRequest request,
            CancellationToken cancellationToken = default);

        [HttpPut]
        public abstract Task<ActionResult<TResponse>> OnUpdateAsync(
            TRequest request,
            CancellationToken cancellationToken = default);

        [HttpDelete]
        public abstract Task<ActionResult<TResponse>> OnDeleteAsync(
            TRequest request,
            CancellationToken cancellationToken = default);
    }
}
