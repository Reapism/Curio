using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Curio.Infrastructure.Identity
{
    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, CurioIdentityDbContext, Guid>
    {
        public ApplicationUserStore(CurioIdentityDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }

        public virtual Task<ApplicationUser> FindByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            return Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber, cancellationToken);
        }
    }
}
