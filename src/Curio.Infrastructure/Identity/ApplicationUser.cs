using System;
using Microsoft.AspNetCore.Identity;

namespace Curio.Infrastructure.Identity
{
    /// <summary>
    /// A custom <see cref="IdentityUser{TKey}"/>.
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>
    {
        public bool HasAgreedToEula { get; set; }
        public bool HasAgreedToPrivacyPolicy { get; set; }
    }
}

