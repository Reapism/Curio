using System;
using System.Threading;
using System.Threading.Tasks;
using Curio.WebApi.Exchanges.Home;
using MediatR;

namespace Curio.WebApi.Features.Home
{
    public class InternalRegistrationHandler : IRequestHandler<InternalRegistrationRequest, RegistrationResponse>
    {
        public Task<RegistrationResponse> Handle(InternalRegistrationRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
