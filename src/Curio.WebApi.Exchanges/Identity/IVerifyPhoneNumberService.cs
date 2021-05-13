using System.Threading;
using System.Threading.Tasks;
using Curio.SharedKernel;
using Curio.WebApi.Exchanges.Identity;

namespace Curio.WebApi.Exchanges.Home
{
    public interface IVerifyPhoneNumberService
    {
        Task<ApiResponse<RegistrationResponse>> VerifyPhoneNumber(VerifyPhoneNumberRequest verifyPhoneNumberRequest, CancellationToken cancellationToken = default);
    }
}
