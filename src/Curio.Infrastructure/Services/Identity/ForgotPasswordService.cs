using System;
using System.Threading.Tasks;
using Curio.Infrastructure.Identity;
using Curio.SharedKernel;
using Curio.WebApi.Exchanges.Home;
using Microsoft.AspNetCore.Identity;
using Curio.SharedKernel.Extensions;
using Curio.Core.Extensions;
using System.Threading;

namespace Curio.Infrastructure.Services.Identity
{
    public class ForgotPasswordService : IForgotPasswordService
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationUserStore applicationUserStore;
        private readonly UserManager<ApplicationUser> userManager;

        public ForgotPasswordService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationUserStore applicationUserStore)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.applicationUserStore = applicationUserStore;
        }

        public async Task<ApiResponse<ForgotPasswordResponse>> ForgotPasswordAsync(ForgotPasswordRequest forgotPasswordRequest, CancellationToken cancellationToken = default)
        {
            var user = await GetUser(forgotPasswordRequest);
            var doesUserExist = DoesUserExist(user);

            if (doesUserExist)
                return ResetPasswordInternal(forgotPasswordRequest);

            // If the user does not exist.
            var response = GetForgotPasswordResponse();

            return response;
        }

        private ApiResponse<ForgotPasswordResponse> ResetPasswordInternal(ForgotPasswordRequest forgotPasswordRequest)
        {
            throw new NotImplementedException();
        }

        private ApiResponse<ForgotPasswordResponse> GetForgotPasswordResponse()
        {
            var response = new ForgotPasswordResponse();
            var apiResponse = response.AsFailedApiValidationResponse(message: "The email or mobile phone is not associated with an account. Please make sure you entered the right email or phone number.");

            return apiResponse;
        }

        private async Task<ApplicationUser> GetUser(ForgotPasswordRequest forgotPasswordRequest)
        {
            ApplicationUser user = null;

            if (forgotPasswordRequest.IsEmailLogin)
                user = await userManager.FindByEmailAsync(forgotPasswordRequest.LoginName);

            if (forgotPasswordRequest.IsMobileLogin)
                user = await applicationUserStore.FindByPhoneNumberAsync(forgotPasswordRequest.LoginName);

            return user;
        }

        private bool DoesUserExist(ApplicationUser user)
        {
            return user is not null;
        }
    }
}
