using System.Threading;
using System.Threading.Tasks;
using Curio.SharedKernel;

namespace Curio.WebApi.Exchanges.Identity
{
    public interface IVerifyEmailService
    {
        Task<ApiResponse<VerifyEmailResponse>> VerifyAsync(VerifyEmailRequest verifyEmailRequest, CancellationToken cancellationToken = default);
    }
}
