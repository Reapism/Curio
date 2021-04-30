using System.Threading.Tasks;
using Curio.SharedKernel;
using Curio.WebApi.Exchanges.Home;

namespace Curio.WebApi.Exchanges.Services.Home
{
    public interface IUserRegistrationService
    {
        Task<ApiResponse<RegistrationResponse>> RegisterUserAsync(EndUserRegistrationRequest registrationRequest);
    }

}
