using System.Threading;
using System.Threading.Tasks;
using Curio.SharedKernel;

namespace Curio.WebApi.Exchanges.Home
{
    public interface IUserRegistrationService
    {
        Task<ApiResponse<RegistrationResponse>> RegisterUserAsync(EndUserRegistrationRequest registrationRequest, CancellationToken cancellationToken = default);
    }

}
