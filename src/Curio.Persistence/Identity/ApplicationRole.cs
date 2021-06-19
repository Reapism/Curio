using System;
using Microsoft.AspNetCore.Identity;

namespace Curio.Persistence.Identity
{
    /// <summary>
    /// A custom <see cref="IdentityRole{TKey}"/>.
    /// </summary>
    public class ApplicationRole : IdentityRole<Guid>
    {
    }
}

