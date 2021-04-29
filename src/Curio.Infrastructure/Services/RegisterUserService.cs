﻿using System;
using System.Threading.Tasks;
using Curio.Core.Entities;
using Curio.Core.Extensions;
using Curio.Core.Interfaces;
using Curio.SharedKernel;
using Curio.SharedKernel.Interfaces;
using Curio.WebApi.Exchanges.Home;
using Curio.WebApi.Exchanges.Services.Home;

namespace Curio.Infrastructure.Services
{
    public class RegisterUserService : IUserRegistrationService
    {
        private readonly IAppLogger<RegisterUserService> logger;
        //TODO: Replace with new Identity entities/services like SignInManager or whatever is needed.
        private readonly IRepository<User> userRepository;
        private readonly IHashingService hashingService;

        public RegisterUserService(IAppLogger<RegisterUserService> logger, IRepository<User> userRepository, IHashingService hashingService)
        {
            this.logger = logger;
            this.userRepository = userRepository;
            this.hashingService = hashingService;
        }

        public async Task<ApiResponse<RegistrationResponse>> RegisterUserAsync(RegistrationRequest registrationRequest)
        {
            var user = GetUser(registrationRequest.Email);
            var userExists = DoesUserExist(user);
            var hasCompletedRegistration = HasCompletedRegistration(user);
            var canRegisterThisUser = !userExists && !hasCompletedRegistration;

            if (canRegisterThisUser)
                await RegisterUserInternal(registrationRequest);

            var response = GetRegistrationResponse(userExists, hasCompletedRegistration);

            return response;
        }

        private async Task RegisterUserInternal(RegistrationRequest registrationRequest)
        {
            var user = NewUser(registrationRequest);

            await userRepository.AddAsync(user);
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

        private User GetUser(string email)
        {
            var user = userRepository
                .Get(e => e.MaskedEmail == email);

            return user;
        }

        private bool DoesUserExist(User user)
        {
            return user is not null;
        }

        private bool HasCompletedRegistration(User user)
        {
            return false;
            // TODO: when this is using the identity model or at least a service in conjuction with it, check whether
            // ApplicationUser.EmailConfirmed
            //return user.IsRegistered;
        }

        public void Sanitize(RegistrationRequest registrationRequest)
        {

        }

        public User NewUser(RegistrationRequest registrationRequest)
        {
            Sanitize(registrationRequest);

            return new User()
            {
                MaskedEmail = registrationRequest.Email,
                // PasswordHash = hashingService.Hash(registrationRequest.Password),
                PasswordLastChangedDate = DateTime.Now,

            }.NewAuditableEntity();
        }
    }
}
