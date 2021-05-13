using System;
using System.Linq;
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
    public class UserRegistrationService<T> : IUserRegistrationService<T>
        where T : RegistrationRequest
    {
        private readonly IAppLogger<UserRegistrationService<T>> logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly PasswordHasher<ApplicationUser> passwordHasher;

        public UserRegistrationService(IAppLogger<UserRegistrationService<T>> logger, UserManager<ApplicationUser> userManager, PasswordHasher<ApplicationUser> passwordHasher)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.passwordHasher = passwordHasher;
        }

        public async Task<ApiResponse<RegistrationResponse>> RegisterUserAsync(T registrationRequest, CancellationToken cancellationToken = default)
        {
            var user = await GetUser(registrationRequest.Email);
            var userExists = DoesUserExist(user);
            var hasCompletedRegistration = HasCompletedRegistration(user);
            var canRegisterThisUser = !userExists && !hasCompletedRegistration;

            if (canRegisterThisUser)
                return await RegisterUserInternal(registrationRequest, cancellationToken);

            // The user cannot be registered for reasons corresponding to the registration process.
            var response = GetRegistrationResponse(userExists, hasCompletedRegistration);

            return response;
        }

        private async Task<ApiResponse<RegistrationResponse>> RegisterUserInternal(T registrationRequest, CancellationToken cancellationToken = default)
        {
            // Sanitize request parameters
            TrySanitize(registrationRequest);
            var user = ToApplicationUser(registrationRequest);

            var identityResult = await userManager.CreateAsync(user, registrationRequest.Password);
            var registrationResponse = GetRegistrationResponse(identityResult);

            return registrationResponse;
        }

        private ApiResponse<RegistrationResponse> GetRegistrationResponse(IdentityResult identityResult)
        {
            var registrationResponse = new RegistrationResponse();

            if (identityResult.Succeeded)
                return registrationResponse.AsSuccessfulApiResponse("The user has been successfully created.");

            var validationToTipsMapping = identityResult.Errors.ToDictionary(k => k.Description, v => v.Description);
            var failedRegistrationResponse = ApiResponseExtensions.AsFailedApiValidationResponse<RegistrationResponse>(validationToTipsMapping, "An error has occured when registering the user");

            return failedRegistrationResponse;
        }

        private ApiResponse<RegistrationResponse> GetRegistrationResponse(bool userExists, bool hasCompletedRegistration)
        {
            var registrationResponse = new RegistrationResponse();

            if (userExists || userExists && hasCompletedRegistration)
            {
                return registrationResponse.AsFailedApiResponse(message: "A user already exists with this email. Maybe try resetting your password.");
            }

            if (userExists && !hasCompletedRegistration)
            {
                return registrationResponse.AsFailedApiResponse(message: "A user already exists with this email but has not completed registration. Please check your email to complete registration");
            }

            return registrationResponse.AsSuccessfulApiResponse();
        }

        private async Task<ApplicationUser> GetUser(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            return user;
        }

        private bool DoesUserExist(ApplicationUser user)
        {
            return user is not null;
        }

        private bool HasCompletedRegistration(ApplicationUser user)
        {
            var emailConfirmed = user?.EmailConfirmed ?? false;
            var mobileConfirmed = user?.PhoneNumberConfirmed ?? false;

            return emailConfirmed || mobileConfirmed;
        }

        private void TrySanitize(T registrationRequest)
        {
            // TODO: Remember this is an API and anything can be sent for these variables,
            // Make sure to completely sanitize each parameter.
        }

        private ApplicationUser ToApplicationUser(T registrationRequest)
        {
            TrySanitize(registrationRequest);
            var emailNormalized = registrationRequest.Email.Normalize().ToUpperInvariant();
            var displayNameNormalized = registrationRequest.DisplayName.Normalize().ToUpperInvariant();

            var user = new ApplicationUser()
            {
                AccessFailedCount = 0,
                HasAgreedToEula = registrationRequest.HasAgreedToEula,
                HasAgreedToPrivacyPolicy = registrationRequest.HasAgreedToPrivacyPolicy,
                ConcurrencyStamp = null, // TODO
                Email = emailNormalized,
                EmailConfirmed = false,
                Id = Guid.NewGuid(),
                LockoutEnabled = false,
                LockoutEnd = null,
                NormalizedEmail = emailNormalized,
                NormalizedUserName = displayNameNormalized,
                PhoneNumber = registrationRequest.HasMobilePhone ? registrationRequest.MobilePhone : null,
                PhoneNumberConfirmed = false, // TODO
                SecurityStamp = null, // TODO
                TwoFactorEnabled = registrationRequest.TwoFactorEnabled,
                UserName = registrationRequest.DisplayName // Case sensitive, display name.
            };

            if (registrationRequest.VerifyWithMobilePhone)
            {
                var c = VerifyPhone();
            }

            var hashedPassword = passwordHasher.HashPassword(user, registrationRequest.Password);
            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, hashedPassword, registrationRequest.Password);
            VerifyHashedPassword(passwordVerificationResult);
            user.PasswordHash = hashedPassword;
            return user;
        }

        private bool VerifyPhone()
        {
            return true;
        }

        private void VerifyHashedPassword(PasswordVerificationResult passwordVerificationResult)
        {
            switch (passwordVerificationResult)
            {
                case PasswordVerificationResult.Success:
                    return;
                case PasswordVerificationResult.Failed:
                    throw new Exception("Password could not be hashed.");
                case PasswordVerificationResult.SuccessRehashNeeded:
                {
                    throw new Exception("Password should be rehashed.");
                }
            }
        }
    }
}
