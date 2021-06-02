using System.Threading;
using System.Threading.Tasks;
using Curio.SharedKernel;
using Curio.WebApi.Exchanges.Identity;
using MediatR;

namespace Curio.WebApi.Handlers.Identity
{
    public class EndUserRegistrationHandler : IRequestHandler<EndUserRegistrationRequest, ApiResponse<RegistrationResponse>>
    {
        private readonly IUserRegistrationService<EndUserRegistrationRequest> userRegistrationService;

        public EndUserRegistrationHandler(IUserRegistrationService<EndUserRegistrationRequest> userRegistrationService)
        {
            this.userRegistrationService = userRegistrationService;
        }

        public async Task<ApiResponse<RegistrationResponse>> Handle(EndUserRegistrationRequest request, CancellationToken cancellationToken)
        {
            return await userRegistrationService.RegisterUserAsync(request);
        }
    }
}
