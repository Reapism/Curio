using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Curio.Domain.Extensions;
using Curio.Persistence.Identity;
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
            var applicationUserResponse = await applicationUserFunction.Invoke();

            if (!applicationUserResponse.IsSuccessful)
                return GetFailedApplicationResponse(applicationUserResponse);

            var userExists = DoesUserExist(applicationUserResponse.Response);

            if (userExists)
                return await LoginAsyncInternal(applicationUserResponse.Response, cancellationToken);

            return GetLoginResponse();
        }

        private async Task<ApiResponse<LoginResponse>> LoginAsyncInternal(ApplicationUser applicationUser, CancellationToken cancellationToken)
        {
            await signInManager.SignInAsync(applicationUser, true);
            return new LoginResponse().AsOkApiResponse();
        }

        private ApiResponse<LoginResponse> GetFailedApplicationResponse<T>(ApiResponse<T> apiResponse)
        {
            var loginResponse = new LoginResponse()
            {
                ReasonByErrorMapping = ("An error occured while retrieving the user.", "The following user was not found.").ToValidationResponse()
            }.AsApiResponse();

            return loginResponse;

        }

        private async Task<Func<Task<ApiResponse<ApplicationUser>>>> GetApplicationUser(LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            return async () =>
            {
                var response = null as ApiResponse<ApplicationUser>;
                if (loginRequest.IsEmailLogin)
                {
                    ValidateEmailLogin(loginRequest.LoginName);
                    var user = await GetApplicationUserFromEmail(loginRequest.LoginName, cancellationToken);
                    if (user is not null)
                        return user.AsOkApiResponse();

                    return user.AsNotFoundApiResponse(overallMessage: $"Unable to find the user with the specified email address \'{loginRequest.LoginName}\'");
                }

                if (loginRequest.IsMobileLogin)
                {
                    ValidationMobilePhoneLogin(loginRequest.LoginName, loginRequest.CountryCode);
                    response = await GetApplicationUserFromMobilePhone(loginRequest.LoginName, cancellationToken);
                }

                return response;
            };
        }

        private bool DoesUserExist(ApplicationUser applicationUser)
        {
            return applicationUser is not null;
        }

        private async Task<ApiResponse<ApplicationUser>> GetApplicationUserFromMobilePhone(string loginName, CancellationToken cancellationToken)
        {
            var response = await applicationUserStore.FindByPhoneNumberAsync(loginName, cancellationToken);
            return response;
        }

        private async Task<ApplicationUser> GetApplicationUserFromEmail(string normailizedEmail, CancellationToken cancellationToken)
        {
            return await applicationUserStore.FindByEmailAsync(normailizedEmail, cancellationToken);
        }

        private ApiResponse<LoginResponse> ValidateEmailLogin(string email)
        {
            var validationResponse = ValidationGuard
                .Against
                .Email(email, nameof(email))
                .AsApiResponse<IValidationResponse, LoginResponse>(null, "The email provided is in an invalid format.");

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
            return new ApiResponse<LoginResponse>(httpStatusCode: (int)HttpStatusCode.Accepted);
        }
    }
}
