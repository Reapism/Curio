using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Curio.WebApi.Endpoints.Base
{
    [ApiController]
    public abstract class CrudEndpoint<TRequest, TResponse> : ControllerBase
    {
        [HttpPost]
        public abstract ActionResult<TResponse> Create(TRequest request);

        [HttpGet]
        public abstract ActionResult<TResponse> Read(TRequest request);

        [HttpPut]
        public abstract ActionResult<TResponse> Update(TRequest request);

        [HttpDelete]
        public abstract ActionResult<TResponse> Delete(TRequest request);
    }

    [ApiController]
    public abstract class CrudEndpointAsync<TRequest, TResponse> : ControllerBase
    {
        [HttpPost]
        public abstract Task<ActionResult<TResponse>> CreateAsync(
            TRequest request,
            CancellationToken cancellationToken = default);

        [HttpGet]
        public abstract Task<ActionResult<TResponse>> ReadAsync(
            TRequest request,
            CancellationToken cancellationToken = default);

        [HttpPut]
        public abstract Task<ActionResult<TResponse>> UpdateAsync(
            TRequest request,
            CancellationToken cancellationToken = default);

        [HttpDelete]
        public abstract Task<ActionResult<TResponse>> DeleteAsync(
            TRequest request,
            CancellationToken cancellationToken = default);
    }
}
