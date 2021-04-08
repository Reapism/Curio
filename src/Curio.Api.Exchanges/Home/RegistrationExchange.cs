using System.Collections.Generic;
using Curio.Api.Exchanges.Bases;
using Curio.SharedKernel.Bases;

namespace Curio.Api.Exchanges.Home
{
    public class RegistrationRequest
    {
        // Login details
        public string Email { get; set; }
        public string Password { get; set; }
        public string MobilePhone { get; set; }

        // Your information
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Profile 
        public string DisplayName { get; set; }
        public byte[] ImageBase64 { get; set; }
    }

    public class RegistrationResponse : ValidationResponse
    {

    }
}
