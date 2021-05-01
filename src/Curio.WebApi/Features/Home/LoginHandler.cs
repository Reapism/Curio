using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Curio.WebApi.Exchanges.Home;
using Curio.WebApi.Services.Home;
using MediatR;

namespace Curio.WebApi.Features.Home
{
    public class LoginHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly IUserRegistrationService userRegistrationService;

        public LoginHandler(IUserRegistrationService userRegistrationService)
        {
            this.userRegistrationService = userRegistrationService;
        }

        public Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
