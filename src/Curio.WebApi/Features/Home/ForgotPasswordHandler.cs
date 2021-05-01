using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Curio.WebApi.Exchanges.Home;
using MediatR;

namespace Curio.WebApi.Features.Home
{
    public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordRequest, ForgotPasswordResponse>
    {
        public Task<ForgotPasswordResponse> Handle(ForgotPasswordRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
