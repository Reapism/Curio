using Curio.Infrastructure.Data;
using Curio.Persistence.Client;
using Curio.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Curio.IntegrationTests.Data
{
    public abstract class BaseEfRepoTestFixture
    {
        protected CurioClientDbContext _dbContext;

        protected static DbContextOptions<CurioClientDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<CurioClientDbContext>();
            builder.UseInMemoryDatabase("curio")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        protected EfRepository GetRepository()
        {
            var options = CreateNewContextOptions();
            var mockDispatcher = new Mock<IDomainEventDispatcher>();

            _dbContext = new CurioClientDbContext(options, mockDispatcher.Object);
            return new EfRepository(_dbContext);
        }
    }
}
