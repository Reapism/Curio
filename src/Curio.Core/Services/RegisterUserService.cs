using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Curio.Core.Models;
using Curio.SharedKernel.Interfaces;

namespace Curio.Core.Services
{
    public class RegisterUserService
    {
        private readonly IRepository repository;

        public RegisterUserService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ApiResponse<RegistrationResponse>> RegisterUser(RegistrationModel registrationModel)
        {
            
        }

        private bool HasRegistered(string email)
        {
            var user = repository.
        }
    }
}
