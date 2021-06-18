using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Curio.Infrastructure
{
    public static class StartupSetup
    {
        public static void AddDbContext<T>(this IServiceCollection services, string connectionString)
            where T : DbContext
        {
            services.AddDbContext<T>(options =>
                options.UseNpgsql(connectionString));
        }
    }
}
