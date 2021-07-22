using Curio.Persistence.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Curio.Persistence.Tools
{
    /// <summary>
    /// This should only be used to create the db context for migrations.
    /// </summary>
    public class CurioClientDbContextDesignTimeFactory : IDesignTimeDbContextFactory<CurioClientDbContext>
    {
        public CurioClientDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                                .SetBasePath(ProjectPathRetriever.GetWebApiPath())
                                .AddJsonFile("appsettings.json")
                                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<CurioClientDbContext>();

            var connectionString = configuration
                        .GetConnectionString("CurioClientPostgre");

            optionsBuilder.UseNpgsql(connectionString);
            // IDomainEventDispatcher is null because its not important for running commands.
            return new CurioClientDbContext(optionsBuilder.Options, null);
        }
    }
}
