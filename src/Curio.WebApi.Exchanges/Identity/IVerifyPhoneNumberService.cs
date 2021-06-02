using System.Threading;
using System.Threading.Tasks;
using Curio.SharedKernel;

namespace Curio.WebApi.Exchanges.Identity
{
    public interface IVerifyPhoneNumberService
    {
        Task<ApiResponse<VerifyPhoneNumberResponse>> VerifyAsync(VerifyPhoneNumberRequest verifyPhoneNumberRequest, CancellationToken cancellationToken = default);
    }
}
