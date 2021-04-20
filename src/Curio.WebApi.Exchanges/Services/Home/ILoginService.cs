using System.Threading.Tasks;
using Curio.SharedKernel;
using Curio.WebApi.Exchanges.Home;

namespace Curio.WebApi.Exchanges.Services.Home
{
    public interface ILoginService
    {
        Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest loginRequest);
    }

}
