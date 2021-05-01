﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Curio.Core.Extensions;
using Curio.Core.Interfaces;
using Curio.Infrastructure.Identity;
using Curio.SharedKernel;
using Curio.WebApi.Exchanges.Home;
using Microsoft.AspNetCore.Identity;

namespace Curio.Infrastructure.Services.Identity
{
    public class RegisterUserService : IUserRegistrationService
    {
        private readonly IAppLogger<RegisterUserService> logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly PasswordHasher<ApplicationUser> passwordHasher;

        public RegisterUserService(IAppLogger<RegisterUserService> logger, UserManager<ApplicationUser> userManager, PasswordHasher<ApplicationUser> passwordHasher)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.passwordHasher = passwordHasher;
        }

        public async Task<ApiResponse<RegistrationResponse>> RegisterUserAsync(EndUserRegistrationRequest registrationRequest)
        {
            var user = await GetUser(registrationRequest.Email);
            var userExists = DoesUserExist(user);
            var hasCompletedRegistration = HasCompletedRegistration(user);
            var canRegisterThisUser = !userExists && !hasCompletedRegistration;

            if (canRegisterThisUser)
                return await RegisterUserInternal(registrationRequest);

            // Can't register the user, and get the registration response for why
            var response = GetRegistrationResponse(userExists, hasCompletedRegistration);

            return response;
        }

        private async Task<ApiResponse<RegistrationResponse>> RegisterUserInternal(EndUserRegistrationRequest registrationRequest)
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
                return registrationResponse.AsSuccessfulApiResponse();

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
            var userConfirmed = user.EmailConfirmed || user.PhoneNumberConfirmed;
            return userConfirmed;
        }

        public void TrySanitize(EndUserRegistrationRequest registrationRequest)
        {
            // TODO: Remember this is an API and anything can be sent for these variables,
            // Make sure to completely sanitize each parameter.
        }

        public ApplicationUser ToApplicationUser(EndUserRegistrationRequest registrationRequest)
        {
            TrySanitize(registrationRequest);

            var user = new ApplicationUser()
            {
                AccessFailedCount = 0,
                HasAgreedToEula = registrationRequest.HasAgreedToEula,
                HasAgreedToPrivacyPolicy = registrationRequest.HasAgreedToPrivacyPolicy,
                ConcurrencyStamp = null, // TODO
                Email = registrationRequest.Email,
                EmailConfirmed = false,
                Id = Guid.NewGuid(),
                LockoutEnabled = false,
                LockoutEnd = null,
                NormalizedEmail = registrationRequest.Email,
                NormalizedUserName = registrationRequest.DisplayName,
                PhoneNumber = registrationRequest.HasMobilePhone ? registrationRequest.MobilePhone : "",
                PhoneNumberConfirmed = false, // TODO
                SecurityStamp = null, // TODO
                TwoFactorEnabled = registrationRequest.TwoFactorEnabled,
                UserName = registrationRequest.DisplayName
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
