using Curio.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Curio.Persistence.Tools
{
    public class CurioIdentityDbContextDesignTimeFactory : IDesignTimeDbContextFactory<CurioIdentityDbContext>
    {
        public CurioIdentityDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                                .SetBasePath(ProjectPathRetriever.GetWebApiPath()) // TODO: need to confirm with relative pathing if this will work also on release
                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<CurioIdentityDbContext>();

            var connectionString = configuration
                        .GetConnectionString("CurioIdentityPostgre");

            optionsBuilder.UseNpgsql(connectionString);

            return new CurioIdentityDbContext(optionsBuilder.Options);
        }

    }
}
