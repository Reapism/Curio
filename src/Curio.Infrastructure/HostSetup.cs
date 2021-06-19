using System;
using Curio.ApplicationCore.Interfaces;
using Curio.Persistence.Client;
using Curio.Persistence.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Curio.Infrastructure
{
    public class HostSetup
    {
        public static void InitializeAndEnsureDatabasesAreCreated(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var curioClientContext = services.GetRequiredService<CurioClientDbContext>();
                    var doesCurioClientDbExist = curioClientContext.Database.EnsureCreated();

                    var curioIdentityContext = services.GetRequiredService<CurioIdentityDbContext>();
                    var doesCurioIdentityDbExist = curioIdentityContext.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<IAppLogger<HostSetup>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
        }
    }
}
