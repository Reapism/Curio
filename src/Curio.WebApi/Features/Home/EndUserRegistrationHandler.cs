using System.Threading;
using System.Threading.Tasks;
using Curio.SharedKernel;
using Curio.WebApi.Exchanges.Home;
using MediatR;

namespace Curio.WebApi.Features.Home
{
    public class EndUserRegistrationHandler : IRequestHandler<EndUserRegistrationRequest, ApiResponse<RegistrationResponse>>
    {
        private readonly IUserRegistrationService userRegistrationService;

        public EndUserRegistrationHandler(IUserRegistrationService userRegistrationService)
        {
            this.userRegistrationService = userRegistrationService;
        }

        public async Task<ApiResponse<RegistrationResponse>> Handle(EndUserRegistrationRequest request, CancellationToken cancellationToken)
        {
            return await userRegistrationService.RegisterUserAsync(request);
        }
    }
}
