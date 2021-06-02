using System.Threading;
using System.Threading.Tasks;
using Curio.SharedKernel;

namespace Curio.WebApi.Exchanges.Identity
{
    public interface IMobileCodeSenderService
    {
        Task<ApiResponse<bool>> SendCodeAsync(MobileCodeSenderRequest mobileCodeSenderRequest, CancellationToken cancellationToken = default);
    }
}
