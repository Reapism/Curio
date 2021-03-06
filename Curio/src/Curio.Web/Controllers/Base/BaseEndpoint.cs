using Microsoft.AspNetCore.Mvc;

namespace Curio.Web.Controllers.Base
{
    /// <summary>
    /// A base class for an endpoint that accepts parameters.
    /// </summary>
    /// <typeparam name="TRequest">The request type.</typeparam>
    /// <typeparam name="TResponse">The response type</typeparam>
    /// <example>
    /// public class Video : BaseEndpoint
    ///     .WithRequest<RequestType>
    ///     .WithResponse<ResponseType>
    /// </example>
    public static class BaseEndpoint
    {
        public static class WithRequest<TRequest>
        {
            public abstract class WithResponse<TResponse> : BaseEndpointSync
            {
                public abstract ActionResult<TResponse> Handle(TRequest request);
            }

            public abstract class WithoutResponse : BaseEndpointSync
            {
                public abstract ActionResult Handle(TRequest request);
            }
        }

        public static class WithoutRequest
        {
            public abstract class WithResponse<TResponse> : BaseEndpointSync
            {
                public abstract ActionResult<TResponse> Handle();
            }

            public abstract class WithoutResponse : BaseEndpointSync
            {
                public abstract ActionResult Handle();
            }
        }
    }

    /// <summary>
    /// A base class for all synchronous endpoints.
    /// </summary>
	[ApiController]
    public abstract class BaseEndpointSync : ControllerBase
    {
    }
}
