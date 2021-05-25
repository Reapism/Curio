using System;
using System.Threading.Tasks;
using Curio.Core.Extensions;
using Curio.Infrastructure.Identity;
using Curio.SharedKernel;
using Curio.SharedKernel.Bases;
using Curio.SharedKernel.Interfaces;
using Curio.WebApi.Exchanges.Identity;
using Microsoft.AspNetCore.Identity;

namespace Curio.Infrastructure.Services.Identity
{
    public class LoginService : ILoginService
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        public LoginService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest loginRequest)
        {
            signInManager.
        }

        private async Task<ApiResponse<LoginResponse>> LoginAsyncInternal(LoginRequest loginRequest)
        {
            var applicationUser = GetApplicationUser(loginRequest).Invoke();
            var userExists = DoesUserExist(applicationUser);

            if (userExists)
                LoginAsyncInternal(applicationUser);
        }

        private async Task<ApiResponse<LoginResponse>> LoginAsyncInternal(ApplicationUser applicationUser)
        {

        }

        private Func<ApplicationUser> GetApplicationUser(LoginRequest loginRequest)
        {
            return () =>
            {
                var user = null as ApplicationUser;
                if (loginRequest.IsEmailLogin)
                {
                    ValidateEmailLogin(loginRequest.LoginName);
                    user = GetApplicationUserFromEmail(loginRequest.LoginName);
                }

                if (loginRequest.IsMobileLogin)
                {
                    ValidationMobilePhoneLogin(loginRequest.LoginName, loginRequest.CountryCode);
                    user = GetApplicationUserFromMobilePhone(loginRequest.LoginName);
                }

                return user;
            };
        }

        private bool DoesUserExist(ApplicationUser applicationUser)
        {
            return applicationUser is not null;
        }

        private ApplicationUser GetApplicationUserFromMobilePhone(string loginName)
        {
            Guard.
        }

        private ApplicationUser GetApplicationUserFromEmail(string loginName)
        {
            throw new NotImplementedException();
        }

        private ApiResponse<LoginResponse> ValidateEmailLogin(string email)
        {
            var validationResponse = ValidationGuard.Against.Email(email, nameof(email)).AsApiResponse<IValidationResponse, LoginResponse>(null, "The email is invalid.");


        }

        private ApiResponse<LoginResponse> ValidationMobilePhoneLogin(string mobilePhone, string countryCode)
        {
            var validationResponse = ValidationGuard.Against.Phone(mobilePhone, countryCode, nameof(mobilePhone));

            mobilePhone
        }
    }
}
