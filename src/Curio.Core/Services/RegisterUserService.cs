using System;
using System.Linq;
using System.Threading.Tasks;
using Curio.Api.Exchanges.Home;
using Curio.Core.Entities;
using Curio.SharedKernel.Interfaces;

namespace Curio.Core.Services
{
    public class RegisterUserService
    {
        private readonly IRepository<User> userRepository;

        public RegisterUserService(IRepository<User> repository)
        {
            this.userRepository = repository;
        }

        public async Task<ApiResponse<RegistrationResponse>> RegisterUser(RegistrationRequest registrationRequest)
        {
            throw new NotImplementedException();
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
    }
}
