using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Curio.WebApi.Exchanges.Home;
using MediatR;

namespace Curio.WebApi.Features.Home
{
    public class AdvertiserRegistrationHandler : IRequestHandler<AdvertiserRegistrationRequest, RegistrationResponse>
    {
        public Task<RegistrationResponse> Handle(AdvertiserRegistrationRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
