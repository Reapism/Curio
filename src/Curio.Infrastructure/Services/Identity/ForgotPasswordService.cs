using System.Threading;
using System.Threading.Tasks;
using Curio.ApplicationCore.Interfaces;
using Curio.Core.Extensions;
using Curio.Infrastructure.Identity;
using Curio.SharedKernel;
using Curio.WebApi.Exchanges.Identity;
using Microsoft.AspNetCore.Identity;

namespace Curio.Infrastructure.Services.Identity
{
    public class ForgotPasswordService : IForgotPasswordService
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationUserStore applicationUserStore;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSender emailSender;
        private readonly IMimeMessageBuilder emailBuilder;

        public ForgotPasswordService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationUserStore applicationUserStore,
            IEmailSender emailSender,
            IMimeMessageBuilder emailBuilder)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.applicationUserStore = applicationUserStore;
            this.emailSender = emailSender;
            this.emailBuilder = emailBuilder;
        }

        public async Task<ApiResponse<ForgotPasswordResponse>> ForgotPasswordAsync(ForgotPasswordRequest forgotPasswordRequest, CancellationToken cancellationToken = default)
        {
            var user = await GetUser(forgotPasswordRequest);
            var doesUserExist = DoesUserExist(user);

            if (doesUserExist)
                return await ResetPasswordInternal(forgotPasswordRequest, user);

            // If the user does not exist.
            var response = GetForgotPasswordResponse();

            return response;
        }

        private async Task<ApiResponse<ForgotPasswordResponse>> ResetPasswordInternal(ForgotPasswordRequest forgotPasswordRequest, ApplicationUser user)
        {
            var passwordResetToken = await userManager.GeneratePasswordResetTokenAsync(user);

            // TODO update to not always return a bad api response.
            return new ForgotPasswordResponse().AsBadRequestApiResponse();
        }

        private ApiResponse<ForgotPasswordResponse> GetForgotPasswordResponse()
        {
            var response = new ForgotPasswordResponse();
            response.ReasonByErrorMapping.Add("Email/Phone not found with a user account.", "Please make sure you entered the right email or phone number.");
            var apiResponse = response.AsNotFoundApiResponse();

            return apiResponse;
        }

        private async Task<ApplicationUser> GetUser(ForgotPasswordRequest forgotPasswordRequest)
        {
            ApplicationUser user = null;
            var response = null as ApiResponse<ApplicationUser>;

            if (forgotPasswordRequest.IsEmailLogin)
                user = await userManager.FindByEmailAsync(forgotPasswordRequest.LoginName);

            if (forgotPasswordRequest.IsMobileLogin)
            {
                response = await applicationUserStore.FindByPhoneNumberAsync(forgotPasswordRequest.LoginName);
                if (response.IsSuccessful)
                    return response.Response;
                else
                    return null;
            }

            return user;
        }

        private bool DoesUserExist(ApplicationUser user)
        {
            return user is not null;
        }
    }
}
