using System.Threading;
using System.Threading.Tasks;
using Curio.SharedKernel;

namespace Curio.WebApi.Exchanges.Home
{
    public interface IUserRegistrationService<in T>
        where T : RegistrationRequest
    {
        Task<ApiResponse<RegistrationResponse>> RegisterUserAsync(T registrationRequest, CancellationToken cancellationToken = default);
    }

}
