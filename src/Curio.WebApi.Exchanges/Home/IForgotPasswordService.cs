using System.Threading.Tasks;
using Curio.SharedKernel;

namespace Curio.WebApi.Exchanges.Home
{
    public interface IForgotPasswordService
    {
        Task<ApiResponse<ForgotPasswordResponse>> ForgotPassword(ForgotPasswordRequest forgotPasswordRequest);
    }

}
