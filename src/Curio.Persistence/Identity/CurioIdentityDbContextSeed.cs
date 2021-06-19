using System.Threading.Tasks;
using Curio.SharedKernel.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Curio.Persistence.Identity
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
            await roleManager.CreateAsync(new IdentityRole(UserTypeConstants.Administrator));

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
            await userManager.AddToRoleAsync(adminUser, UserTypeConstants.Administrator);

            endUser = await userManager.FindByNameAsync(AuthorizationSeededUsers.EndUser.Item1);
            await userManager.AddToRoleAsync(endUser, UserTypeConstants.EndUser);

            advertiserUser = await userManager.FindByNameAsync(AuthorizationSeededUsers.Advertiser.Item1);
            await userManager.AddToRoleAsync(advertiserUser, UserTypeConstants.Advertiser);

            internalUser = await userManager.FindByNameAsync(AuthorizationSeededUsers.Internal.Item1);
            await userManager.AddToRoleAsync(internalUser, UserTypeConstants.Internal);
        }
    }
}
