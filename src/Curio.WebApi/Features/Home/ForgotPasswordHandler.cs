using System.Threading;
using System.Threading.Tasks;
using Curio.SharedKernel;
using Curio.WebApi.Exchanges.Home;
using MediatR;

namespace Curio.WebApi.Features.Home
{
    public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordRequest, ApiResponse<ForgotPasswordResponse>>
    {
        private readonly IForgotPasswordService forgotPasswordService;

        public ForgotPasswordHandler(IForgotPasswordService forgotPasswordService)
        {
            this.forgotPasswordService = forgotPasswordService;
        }

        public async Task<ApiResponse<ForgotPasswordResponse>> Handle(ForgotPasswordRequest request, CancellationToken cancellationToken)
        {
            return await forgotPasswordService.ForgotPasswordAsync(request, cancellationToken);
        }
    }
}
