using System.Threading;
using System.Threading.Tasks;
using Curio.SharedKernel;
using Curio.WebApi.Exchanges.Identity;
using MediatR;

namespace Curio.WebApi.Handlers.Identity
{
    public class InternalRegistrationHandler : IRequestHandler<InternalRegistrationRequest, ApiResponse<RegistrationResponse>>
    {
        private readonly IUserRegistrationService<InternalRegistrationRequest> userRegistrationService;

        public InternalRegistrationHandler(IUserRegistrationService<InternalRegistrationRequest> userRegistrationService)
        {
            this.userRegistrationService = userRegistrationService;
        }

        public async Task<ApiResponse<RegistrationResponse>> Handle(InternalRegistrationRequest request, CancellationToken cancellationToken)
        {
            return await userRegistrationService.RegisterUserAsync(request);
        }
    }
}
