using System;
using System.Threading;
using System.Threading.Tasks;
using Curio.WebApi.Exchanges.Home;
using MediatR;

namespace Curio.WebApi.Features.Home
{
    public class AdministratorRegistrationHandler : IRequestHandler<AdministratorRegistrationRequest, RegistrationResponse>
    {
        public Task<RegistrationResponse> Handle(AdministratorRegistrationRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
