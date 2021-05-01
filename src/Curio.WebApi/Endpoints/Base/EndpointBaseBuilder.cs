using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Curio.Api.Endpoints.Base
{
    /// <summary>
    /// A class exposing actionable endpoints that can be used
    /// for building RESTful API services without the use of MediatR.
    /// </summary>
    /// <typeparam name="TRequest">The request type.</typeparam>
    /// <typeparam name="TResponse">The response type</typeparam>
    public static class EndpointBaseBuilder
    {
        public static class WithRequest<TRequest>
        {
            public abstract class WithResponse<TResponse> : EndpointBase
            {
                public abstract ActionResult<TResponse> Handle(TRequest request);
            }

            public abstract class WithoutResponse : EndpointBase
            {
                public abstract ActionResult Handle(TRequest request);
            }
        }

        public static class WithoutRequest
        {
            public abstract class WithResponse<TResponse> : EndpointBase
            {
                public abstract ActionResult<TResponse> Handle();
            }

            public abstract class WithoutResponse : EndpointBase
            {
                public abstract ActionResult Handle();
            }
        }
    }

    /// <summary>
    /// A base class for all synchronous endpoints.
    /// </summary>
	[ApiController]
    public abstract class EndpointBase : ControllerBase
    {
    }

    /// <summary>
    /// A class exposing actionable endpoints that can be used
    /// for building RESTful API services without the use of MediatR.
    /// </summary>
    /// <typeparam name="TRequest">The request type.</typeparam>
    /// <typeparam name="TResponse">The response type</typeparam>
    public static class EndpointBaseAsyncBuilder
    {
        public static class WithRequest<TRequest>
        {
            public abstract class WithResponse<TResponse> : EndpointBaseAsync
            {
                public abstract Task<ActionResult<TResponse>> HandleAsync(
                    TRequest request,
                    CancellationToken cancellationToken = default
                );
            }

            public abstract class WithoutResponse : EndpointBaseAsync
            {
                public abstract Task<ActionResult> HandleAsync(
                    TRequest request,
                    CancellationToken cancellationToken = default
                );
            }
        }

        public static class WithoutRequest
        {
            public abstract class WithResponse<TResponse> : EndpointBaseAsync
            {
                public abstract Task<ActionResult<TResponse>> HandleAsync(
                    CancellationToken cancellationToken = default
                );
            }

            public abstract class WithoutResponse : EndpointBaseAsync
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
    public abstract class EndpointBaseAsync : ControllerBase
    {
    }
}
