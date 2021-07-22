using Autofac.Extensions.DependencyInjection;
using Curio.Infrastructure.Setup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Curio.WebApi
{
    public class Program
    {
        /// <summary>Main entry point for the web api.</summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            InitializeDatabaseState(host);

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host
                .CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                    // Configure more things here.
                    .UseStartup<Startup>()
                    .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.AddConsole();
                    });
            });

            return hostBuilder;
        }

        private static void InitializeDatabaseState(IHost host)
        {
            HostSetup.InitializeAndEnsureDatabasesAreCreated(host);
        }
    }
}
