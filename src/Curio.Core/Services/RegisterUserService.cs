using System;
using System.Threading.Tasks;
using Curio.Core.Entities;
using Curio.Core.Extensions;
using Curio.Core.Interfaces;
using Curio.SharedKernel;
using Curio.SharedKernel.Interfaces;
using Curio.WebApi.Exchanges.Home;
using Curio.WebApi.Exchanges.Services.Home;

namespace Curio.Core.Services
{
    public class RegisterUserService : IUserRegistrationService
    {
        private readonly IRepository<User> userRepository;
        private readonly IHashingService hashingService;

        public RegisterUserService(IRepository<User> repository, IHashingService hashingService, IMobileCodeSubmitter)
        {
            this.userRepository = repository;
            this.hashingService = hashingService;
        }

        public async Task<ApiResponse<RegistrationResponse>> RegisterUser(RegistrationRequest registrationRequest)
        {
            var user = GetUser(registrationRequest.Email);
            var userExists = DoesUserExist(user);
            var hasCompletedRegistration = HasCompletedRegistration(user);
            var canRegisterThisUser = !userExists && !hasCompletedRegistration;

            if (canRegisterThisUser)
                await RegisterUserInternal(registrationRequest);

            var response = GetRegistrationResponse(userExists, hasCompletedRegistration, registrationRequest.Email);

            return response;
        }

        private async Task RegisterUserInternal(RegistrationRequest registrationRequest)
        {
            var user = NewUser(registrationRequest);

            await userRepository.AddAsync(user);
        }

        private ApiResponse<RegistrationResponse> GetRegistrationResponse(bool userExists, bool hasCompletedRegistration, string email)
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
                .Get(e => e.Email == email);

            return user;
        }

        private bool DoesUserExist(User user)
        {
            return user is not null;
        }

        private bool HasCompletedRegistration(User user)
        {
            return user.IsRegistered;
        }

        public void Sanitize(RegistrationRequest registrationRequest)
        {

        }

        public  (RegistrationRequest registrationRequest)
        {
            if (registrationRequest.HasMobilePhone)
                LoginType.
        }

        public User NewUser(RegistrationRequest registrationRequest)
        {
            Sanitize(registrationRequest);

            return new User()
            {
                Email = registrationRequest.Email,
                PasswordHash = hashingService.Hash(registrationRequest.Password),
                PasswordLastChangedDate = DateTime.Now,
                LoginType =
            }.NewAuditableEntity();
        }
    }
}
