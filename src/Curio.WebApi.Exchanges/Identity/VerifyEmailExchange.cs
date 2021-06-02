using Curio.SharedKernel;
using Curio.SharedKernel.Bases;
using MediatR;

namespace Curio.WebApi.Exchanges.Identity
{
    public class VerifyEmailRequest : IRequest<ApiResponse<VerifyEmailResponse>>
    {
    }

    public class VerifyEmailResponse : ValidationResponse
    {
    }
}
