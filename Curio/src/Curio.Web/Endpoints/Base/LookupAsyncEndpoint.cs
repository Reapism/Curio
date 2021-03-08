using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Curio.Web.Endpoints.Base
{
    // TODO
    // reason how to create CRUD and lookup endpoints
    // with a similar syntax to the base endpoint class
    // or a new one that automatically gives you methods
    // with request/response depending on lookup context.
    public abstract class LookupAsyncEndpoint
    {
        // lookup context will require a request since its
        // it needs a guid.
        public Task<TResponse> HandleLookupAsync<TRequest, TResponse>(
            TRequest request, 
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(default(TResponse));
        }
    }
}
