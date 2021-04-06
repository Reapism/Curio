using System;
using System.Linq;
using System.Threading.Tasks;
using Curio.Core.Entities;
using Curio.Core.Models;
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

        public async Task<ApiResponse<RegistrationResponse>> RegisterUser(RegistrationModel registrationModel)
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
