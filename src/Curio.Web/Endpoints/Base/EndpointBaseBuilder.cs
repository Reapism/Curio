using System.Threading;
using System.Threading.Tasks;
using Curio.SharedKernel;
using Microsoft.AspNetCore.Mvc;

namespace Curio.Web.Endpoints.Base
{
    /// <summary>
    /// A class exposing actionable endpoints that can be used
    /// for RESTful API services.
    /// </summary>
    /// <typeparam name="TRequest">The request type.</typeparam>
    /// <typeparam name="TResponse">The response type.</typeparam>
    public static class EndpointBaseBuilder
    {
        public static class WithRequest<TRequest>
        {
            public abstract class WithResponse<TResponse> : BaseEndpoint
            {
                public abstract ActionResult<ApiResponse<TResponse>> Handle(TRequest request);
            }

            public abstract class WithoutResponse : BaseEndpoint
            {
                public abstract ActionResult Handle(TRequest request);
            }
        }

        public static class WithoutRequest
        {
            public abstract class WithResponse<TResponse> : BaseEndpoint

            {
                public abstract ActionResult<ApiResponse<TResponse>> Handle();
            }

            public abstract class WithoutResponse : BaseEndpoint
            {
                public abstract ActionResult Handle();
            }
        }
    }

    /// <summary>
    /// A base class for all synchronous endpoints.
    /// </summary>
	[ApiController]
    public abstract class BaseEndpoint : ControllerBase
    {
    }

    /// <summary>
    /// A base class for an endpoint that accepts parameters.
    /// </summary>
    /// <typeparam name="TRequest">The request type.</typeparam>
    /// <typeparam name="TResponse">The response type</typeparam>
    public static class EndpointBaseAsyncBuilder
    {
        public static class WithRequest<TRequest>
        {
            public abstract class WithResponse<TResponse> : BaseEndpointAsync
            {
                public abstract Task<ActionResult<ApiResponse<TResponse>>> HandleAsync(
                    TRequest request,
                    CancellationToken cancellationToken = default
                );
            }

            public abstract class WithoutResponse : BaseEndpointAsync
            {
                public abstract Task<ActionResult> HandleAsync(
                    TRequest request,
                    CancellationToken cancellationToken = default
                );
            }
        }

        public static class WithoutRequest
        {
            public abstract class WithResponse<TResponse> : BaseEndpointAsync
            {
                public abstract Task<ActionResult<ApiResponse<TResponse>>> HandleAsync(
                    CancellationToken cancellationToken = default
                );
            }

            public abstract class WithoutResponse : BaseEndpointAsync
            {
                public abstract Task<ActionResult> HandleAsync(
                    CancellationToken cancellationToken = default
                );
            }
        }
    }

    /// <summary>
    /// A base class for all asynchronous endpoints.
    /// </summary>
    [ApiController]
    public abstract class BaseEndpointAsync : ControllerBase
    {
    }
}
