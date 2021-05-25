using System;
using System.Threading.Tasks;
using Curio.Infrastructure.Identity;
using Curio.SharedKernel;
using Curio.SharedKernel.Bases;
using Curio.WebApi.Exchanges.Identity;
using Microsoft.AspNetCore.Identity;

namespace Curio.Infrastructure.Services.Identity
{
    public class LoginService : ILoginService
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        public LoginService(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public async Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest loginRequest)
        {

        }

        private async Task<ApiResponse<LoginResponse>> LoginAsyncInternal(LoginRequest loginRequest)
        {
            Action loginAction = GetLoginAction(loginRequest);

            loginAction.Invoke();
        }

        private Action GetLoginAction(LoginRequest loginRequest)
        {
            return () =>
            {
                if (loginRequest.IsEmailLogin)
                    LoginUsingEmail(loginRequest.LoginName);

                if (loginRequest.IsMobileLogin)
                    LoginUsingMobile(loginRequest.LoginName, loginRequest.CountryCode);

                return;
            };
        }

        private ApiResponse<LoginResponse> LoginUsingEmail(string email)
        {
            ValidationGuard.Against.Email(email, nameof(email));

        }

        private ApiResponse<LoginResponse> LoginUsingMobile(string mobilePhone, string countryCode)
        {
            ValidationGuard.Against.Phone(mobilePhone, countryCode, nameof(mobilePhone));

            mobilePhone
        }
    }
}
