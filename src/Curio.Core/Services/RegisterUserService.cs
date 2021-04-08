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
            throw new Exception();
        }

        private bool HasRegistered(string email)
        {
            var user = userRepository
                .List(e => e.Email == email)
                .FirstOrDefault();

            return user is not null;
        }
    }
}
