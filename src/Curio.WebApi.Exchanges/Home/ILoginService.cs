using System.Threading.Tasks;
using Curio.SharedKernel;

namespace Curio.WebApi.Exchanges.Home
{
    public interface ILoginService
    {
        Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest loginRequest);
    }

}
