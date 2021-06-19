using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Curio.ApplicationCore.Interfaces;
using Curio.Core.Entities;
using Curio.Core.Extensions;
using Curio.Persistence.Identity;
using Curio.SharedKernel;
using Curio.SharedKernel.Interfaces;
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
        private readonly IHashingService hashingService;
        private readonly IRepository<UserProfile> userProfileRepository;

        public UserRegistrationService(
            IAppLogger<UserRegistrationService<T>> logger,
            UserManager<ApplicationUser> userManager,
            PasswordHasher<ApplicationUser> passwordHasher,
            IHashingService hashingService,
            IRepository<UserProfile> userProfileRepository)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.passwordHasher = passwordHasher;
            this.hashingService = hashingService;
            this.userProfileRepository = userProfileRepository;
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
            var user = SetupApplicationUserFromRequest(registrationRequest);
            var userProfile = CreateUserProfile(registrationRequest, user);

            var identityResult = await userManager.CreateAsync(user, registrationRequest.Password);
            var registrationResponse = GetRegistrationResponse(identityResult);

            _ = await userProfileRepository.AddAsync(userProfile);

            return registrationResponse;
        }

        private ApiResponse<RegistrationResponse> GetRegistrationResponse(IdentityResult identityResult)
        {
            var registrationResponse = new RegistrationResponse();

            if (identityResult.Succeeded)
                return registrationResponse.AsOkApiResponse("The user has been successfully created.");

            var validationToTipsMapping = identityResult.Errors.ToDictionary(k => k.Description, v => v.Description);
            var failedRegistrationResponse = ApiResponseExtensions.AsApiResponse<RegistrationResponse>(validationToTipsMapping, "An error has occured when registering the user");

            return failedRegistrationResponse;
        }

        private ApiResponse<RegistrationResponse> GetRegistrationResponse(bool userExists, bool hasCompletedRegistration)
        {
            var registrationResponse = new RegistrationResponse();

            if (userExists || userExists && hasCompletedRegistration)
            {
                registrationResponse.ReasonByErrorMapping.Add("User already exists", "A user already exists with this email. Maybe try resetting your password.");
                return registrationResponse.AsBadRequestApiResponse();
            }

            if (userExists && !hasCompletedRegistration)
            {
                registrationResponse.ReasonByErrorMapping.Add("User already exists", "A user already exists with this email but has not completed registration. Please check your email to complete registration.");
                return registrationResponse.AsBadRequestApiResponse();
            }

            return registrationResponse.AsOkApiResponse();
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
            var requestSanitizer = GetRequestSanitizer(registrationRequest);

            requestSanitizer.Invoke();
        }

        private Action GetRequestSanitizer(T registrationRequest)
        {
            // TODO: Remember this is an API and anything can be sent for these variables,
            // Make sure to completely sanitize each parameter.
            return new Action(() =>
            {
                registrationRequest.DisplayName = registrationRequest.DisplayName.Normalize().Trim();
                registrationRequest.Email = registrationRequest.Email.Normalize().Trim().ToUpperInvariant();
                registrationRequest.FirstName = registrationRequest.FirstName.Normalize().Trim();
                registrationRequest.LastName = registrationRequest.LastName.Normalize().Trim();
                registrationRequest.MobilePhone = registrationRequest.MobilePhone.Normalize().Trim();
                registrationRequest.UniqueHandle = registrationRequest.UniqueHandle.Normalize().Trim();
            });
        }

        private ApplicationUser SetupApplicationUserFromRequest(T registrationRequest)
        {
            TrySanitize(registrationRequest);
            var applicationUser = CreateApplicationUserFromRequest(registrationRequest);

            if (registrationRequest.VerifyWithMobilePhone)
            {
                var c = VerifyPhone();
            }

            applicationUser.PasswordHash = GetHashedPassword(registrationRequest, applicationUser);
            return applicationUser;
        }

        private UserProfile CreateUserProfile(T registrationRequest, ApplicationUser applicationUser)
        {
            var userProfileId = Guid.NewGuid();
            var userProfile = new UserProfile()
            {
                UserAddress = CreateUserAddress(registrationRequest, applicationUser, userProfileId),
                ReferenceId = applicationUser.Id,
                ReferenceName = "Users",
                DisplayName = registrationRequest.DisplayName,
                UniqueHandle = registrationRequest.UniqueHandle,
            }.NewAuditableEntity();

            return userProfile;
        }

        private UserAddress CreateUserAddress(T registrationRequest, ApplicationUser applicationUser, Guid userProfileId)
        {
            var userAddress = new UserAddress()
            {
                Address = null,
                City = null,
                Country = null,
                FirstName = registrationRequest.FirstName,
                LastName = registrationRequest.LastName,
                PostalCode = null,
                State = null,
                ZipCode = null,
                UserProfileId = userProfileId
            }
            .NewAuditableEntity();

            return userAddress;
        }

        private ApplicationUser CreateApplicationUserFromRequest(T registrationRequest)
        {
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

            return user;
        }

        private string GetHashedPassword(T registrationRequest, ApplicationUser user)
        {
            var hashedPassword = passwordHasher.HashPassword(user, registrationRequest.Password);
            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, hashedPassword, registrationRequest.Password);
            VerifyHashedPassword(passwordVerificationResult);
            return hashedPassword;
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
                case PasswordVerificationResult.SuccessRehashNeeded:
                {
                    logger.LogWarning("A password was just hashed successfully, but using a deprecated algorithm. Recommendation is to rehash and update.");
                    return;
                }
                case PasswordVerificationResult.Failed:
                    throw new Exception("Password could not be hashed.");
            }
        }
    }
}
