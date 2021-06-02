using System.Threading;
using System.Threading.Tasks;
using Curio.SharedKernel;
using Curio.WebApi.Exchanges.Identity;
using MediatR;

namespace Curio.WebApi.Handlers.Identity
{
    public class AdministratorRegistrationHandler : IRequestHandler<AdministratorRegistrationRequest, ApiResponse<RegistrationResponse>>
    {
        private readonly IUserRegistrationService<AdministratorRegistrationRequest> userRegistrationService;

        public AdministratorRegistrationHandler(IUserRegistrationService<AdministratorRegistrationRequest> userRegistrationService)
        {
            this.userRegistrationService = userRegistrationService;
        }

        public async Task<ApiResponse<RegistrationResponse>> Handle(AdministratorRegistrationRequest request, CancellationToken cancellationToken)
        {
            return await userRegistrationService.RegisterUserAsync(request);
        }
    }
}
