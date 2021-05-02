using System;
using System.Threading;
using System.Threading.Tasks;
using Curio.SharedKernel;
using Curio.WebApi.Exchanges.Home;
using MediatR;

namespace Curio.WebApi.Features.Home
{
    public class InternalRegistrationHandler : IRequestHandler<InternalRegistrationRequest, ApiResponse<RegistrationResponse>>
    {
        public Task<ApiResponse<RegistrationResponse>> Handle(InternalRegistrationRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
