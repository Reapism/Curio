using System;
using System.Linq;
using Curio.Infrastructure.Data;
using Curio.Infrastructure.Identity;
using Curio.SharedKernel.Interfaces;
using Curio.UnitTests;
using Curio.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Curio.FunctionalTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .UseSolutionRelativeContentRoot("src/Curio.Web")
                .ConfigureServices(services =>
                {
                    // Replace registered db contexts with in-memory ones.
                    ReplaceCurioClientDbContext(services);
                    ReplaceCurioIdentityDbContext(services);

                    // Register a domain event dispatcher that never fails.
                    services.AddScoped<IDomainEventDispatcher, NoOpDomainEventDispatcher>();

                    // Build the service provider.
                    var serviceProvider = services.BuildServiceProvider();

                    EnsureDatabasesAreCreated(serviceProvider);
                });
        }

        private void EnsureDatabasesAreCreated(ServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var curioClientDb = scopedServices.GetRequiredService<CurioClientDbContext>();
                var curioIdentityDb = scopedServices.GetRequiredService<CurioIdentityDbContext>();

                var logger = scopedServices
                    .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                // Ensure the database is created.
                var clientDbExists = curioClientDb.Database.EnsureCreated();
                var identityDbExists = curioIdentityDb.Database.EnsureCreated();

                logger.LogInformation($"Curio Client database exists: {clientDbExists}");
                logger.LogInformation($"Curio Identity database exists: {clientDbExists}");

                logger.LogInformation("If databases dont exist, they have been created.");

                try
                {
                    // Seed the database with test data.
                    //SeedData.PopulateTestData(db);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred seeding the " +
                        $"database with test messages. Error: {ex.Message}");
                }
            }
        }

        private void ReplaceCurioIdentityDbContext(IServiceCollection services)
        {
            // Remove the app's CurioIdentityDbContext registration.
            var curioIdentityDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<CurioIdentityDbContext>));

            if (curioIdentityDescriptor is not null)
            {
                services.Remove(curioIdentityDescriptor);
            }
            services.AddDbContext<CurioIdentityDbContext>(options =>
            {
                options.UseInMemoryDatabase("curio-identity-in-memory");
            });

            // Add CurioIdentityDbContext using an in-memory database for testing.
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                    .AddEntityFrameworkStores<CurioIdentityDbContext>()
                    .AddUserStore<ApplicationUserStore>()
                    .AddRoleStore<ApplicationRole>()
                    .AddDefaultTokenProviders();

        }

        private void ReplaceCurioClientDbContext(IServiceCollection services)
        {
            // Remove the app's CurioClientDbContext registration.
            var curioClientDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<CurioClientDbContext>));

            if (curioClientDescriptor is not null)
            {
                services.Remove(curioClientDescriptor);
            }

            // Add CurioClientDbContext using an in-memory database for testing.
            services.AddDbContext<CurioClientDbContext>(options =>
            {
                options.UseInMemoryDatabase("curio-client-in-memory");
            });
        }
    }
}
