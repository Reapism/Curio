using System.Threading;
using System.Threading.Tasks;
using Curio.SharedKernel;
using Curio.WebApi.Exchanges.Identity;
using MediatR;

namespace Curio.WebApi.Handlers.Identity
{
    public class AdvertiserRegistrationHandler : IRequestHandler<AdvertiserRegistrationRequest, ApiResponse<RegistrationResponse>>
    {
        private readonly IUserRegistrationService<AdvertiserRegistrationRequest> userRegistrationService;

        public AdvertiserRegistrationHandler(IUserRegistrationService<AdvertiserRegistrationRequest> userRegistrationService)
        {
            this.userRegistrationService = userRegistrationService;
        }

        public async Task<ApiResponse<RegistrationResponse>> Handle(AdvertiserRegistrationRequest request, CancellationToken cancellationToken)
        {
            return await userRegistrationService.RegisterUserAsync(request);
        }
    }
}
