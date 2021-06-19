using System;
using System.Threading;
using System.Threading.Tasks;
using Curio.Core.Extensions;
using Curio.SharedKernel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Curio.Persistence.Identity
{
    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, CurioIdentityDbContext, Guid>
    {
        public ApplicationUserStore(CurioIdentityDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }

        /// <summary>
        /// Finds an <see cref="ApplicationUser"/> by phonenumber.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ApiResponse<ApplicationUser>> FindByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            try
            {
                var applicationUser = await Users.SingleOrDefaultAsync(u => u.PhoneNumber == phoneNumber, cancellationToken);
                if (applicationUser is null)
                    return default(ApplicationUser).AsNotFoundApiResponse($"No user was found with the given phonenumber \'{phoneNumber}\'");

                return applicationUser.AsOkApiResponse();
            }
            catch (InvalidOperationException ioe)
            {
                return default(ApplicationUser).AsBadRequestApiResponse("More than one user was found with this phonenumber", ioe);
            }
        }
    }
}
