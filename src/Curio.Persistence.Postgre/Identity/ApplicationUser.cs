using System;
using Microsoft.AspNetCore.Identity;

namespace Curio.Persistence.Postgre.Identity
{
    /// <summary>
    /// A custom <see cref="IdentityUser{TKey}"/>.
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>
    {
        public bool HasAgreedToEula { get; set; }
        public bool HasAgreedToPrivacyPolicy { get; set; }
        public bool HasLoggedInAfterRegistration { get; set; }
    }
}

