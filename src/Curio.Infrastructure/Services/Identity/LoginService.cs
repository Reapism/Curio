using System;
using System.Net;
using System.Threading;
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
        private readonly ApplicationUserStore applicationUserStore;

        public LoginService(SignInManager<ApplicationUser> signInManager, ApplicationUserStore applicationUserStore)
        {
            this.signInManager = signInManager;
            this.applicationUserStore = applicationUserStore;
        }

        public async Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken = default)
        {
            return await LoginAsyncInternal(loginRequest, cancellationToken);
        }

        private async Task<ApiResponse<LoginResponse>> LoginAsyncInternal(LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            var applicationUserFunction = await GetApplicationUser(loginRequest, cancellationToken);
            var applicationUser = await applicationUserFunction.Invoke();
            var userExists = DoesUserExist(applicationUser);

            if (userExists)
                return await LoginAsyncInternal(applicationUser, cancellationToken);

            return GetLoginResponse();
        }

        private async Task<ApiResponse<LoginResponse>> LoginAsyncInternal(ApplicationUser applicationUser, CancellationToken cancellationToken)
        {
            await signInManager.SignInAsync(applicationUser, true);
            return new ApiResponse<LoginResponse>(null, (int)HttpStatusCode.OK);
        }

        private async Task<Func<Task<ApplicationUser>>> GetApplicationUser(LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            return async () =>
            {
                var user = null as ApplicationUser;
                if (loginRequest.IsEmailLogin)
                {
                    ValidateEmailLogin(loginRequest.LoginName);
                    user = await GetApplicationUserFromEmail(loginRequest.LoginName, cancellationToken);
                }

                if (loginRequest.IsMobileLogin)
                {
                    ValidationMobilePhoneLogin(loginRequest.LoginName, loginRequest.CountryCode);
                    user = await GetApplicationUserFromMobilePhone(loginRequest.LoginName, cancellationToken);
                }

                return user;
            };
        }

        private bool DoesUserExist(ApplicationUser applicationUser)
        {
            return applicationUser is not null;
        }

        private async Task<ApplicationUser> GetApplicationUserFromMobilePhone(string loginName, CancellationToken cancellationToken)
        {
            return await applicationUserStore.FindByPhoneNumberAsync(loginName, cancellationToken);
        }

        private async Task<ApplicationUser> GetApplicationUserFromEmail(string loginName, CancellationToken cancellationToken)
        {
            return await applicationUserStore.FindByEmailAsync(loginName, cancellationToken);
        }

        private ApiResponse<LoginResponse> ValidateEmailLogin(string email)
        {
            var validationResponse = ValidationGuard
                .Against
                .Email(email, nameof(email))
                .AsApiResponse<IValidationResponse, LoginResponse>(null, "The email is invalid.");

            return validationResponse;
        }

        private ApiResponse<LoginResponse> ValidationMobilePhoneLogin(string mobilePhone, string countryCode)
        {
            // need to check api response to figure out how to properly return this if its success or not. right now it assumes its invalid.
            var validationResponse = ValidationGuard
                .Against
                .Phone(mobilePhone, countryCode, nameof(mobilePhone))
                .AsApiResponse<IValidationResponse, LoginResponse>(null, $"The mobile phone provided is invalid in the specific country. Country code: {countryCode}"); ;

            return validationResponse;
        }

        private ApiResponse<LoginResponse> GetLoginResponse()
        {
            return new ApiResponse<LoginResponse>(null, (int)HttpStatusCode.Accepted);
        }
    }
}
