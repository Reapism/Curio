using System;
using System.Threading.Tasks;
using Curio.SharedKernel;
using Curio.WebApi.Exchanges.Home;

namespace Curio.Infrastructure.Services.Identity
{
    public class ForgotPasswordService : IForgotPasswordService
    {
        public Task<ApiResponse<ForgotPasswordResponse>> ForgotPassword(ForgotPasswordRequest forgotPasswordRequest)
        {
            throw new NotImplementedException();
        }
    }
}
