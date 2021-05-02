using System;
using System.Threading;
using System.Threading.Tasks;
using Curio.SharedKernel;
using Curio.WebApi.Exchanges.Home;
using MediatR;

namespace Curio.WebApi.Features.Home
{
    public class AdministratorRegistrationHandler : IRequestHandler<AdministratorRegistrationRequest, ApiResponse<RegistrationResponse>>
    {
        public Task<ApiResponse<RegistrationResponse>> Handle(AdministratorRegistrationRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
