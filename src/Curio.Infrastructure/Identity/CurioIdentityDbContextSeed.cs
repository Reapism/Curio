using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Curio.SharedKernel.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Curio.Infrastructure.Identity
{
    public class CurioIdentityDbContextSeed
    {
        /// <summary>
        /// For use in development only. A seeded administrator account.
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <returns></returns>
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(AuthorizationRoleConstants.Administrator));

            // Create End user
            var endUser = new ApplicationUser { UserName = AuthorizationSeededUsers.EndUser.Item1, Email = AuthorizationSeededUsers.EndUser.Item1 };
            await userManager.CreateAsync(endUser, AuthorizationSeededUsers.EndUser.Item2);
            
            // Create Admin user
            var adminUser = new ApplicationUser { UserName = AuthorizationSeededUsers.Admin.Item1, Email = AuthorizationSeededUsers.Admin.Item1 };
            await userManager.CreateAsync(adminUser, AuthorizationSeededUsers.Admin.Item2);

            // Create Advertiser user
            var advertiserUser = new ApplicationUser { UserName = AuthorizationSeededUsers.Advertiser.Item1, Email = AuthorizationSeededUsers.Advertiser.Item1 };
            await userManager.CreateAsync(advertiserUser, AuthorizationSeededUsers.Advertiser.Item2);
            
            // Create Internal user
            var internalUser = new ApplicationUser { UserName = AuthorizationSeededUsers.Internal.Item1, Email = AuthorizationSeededUsers.Internal.Item1 };
            await userManager.CreateAsync(internalUser, AuthorizationSeededUsers.Internal.Item2);

            // User
            adminUser = await userManager.FindByNameAsync(AuthorizationSeededUsers.Admin.Item1);
            await userManager.AddToRoleAsync(adminUser, AuthorizationRoleConstants.Administrator);

            endUser = await userManager.FindByNameAsync(AuthorizationSeededUsers.EndUser.Item1);
            await userManager.AddToRoleAsync(endUser, AuthorizationRoleConstants.EndUser);

            advertiserUser = await userManager.FindByNameAsync(AuthorizationSeededUsers.Advertiser.Item1);
            await userManager.AddToRoleAsync(advertiserUser, AuthorizationRoleConstants.Advertiser);

            internalUser = await userManager.FindByNameAsync(AuthorizationSeededUsers.Internal.Item1);
            await userManager.AddToRoleAsync(internalUser, AuthorizationRoleConstants.Internal);
        }
    }
}
