﻿using Curio.SharedKernel.Bases;

namespace Curio.WebApi.Exchanges.Home
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

        // Computed values from client side
        public bool HasMobilePhone { get; set; }
    }

    public class RegistrationResponse : ValidationResponse
    {

    }
}