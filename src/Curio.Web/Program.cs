using System;
using Autofac.Extensions.DependencyInjection;
using Curio.ApplicationCore.Interfaces;
using Curio.Infrastructure;
using Curio.Persistence.Client;
using Curio.Persistence.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Curio.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            InitializeDatabaseState(host);

            host.Run();
        }

        private static void InitializeDatabaseState(IHost host)
        {
            HostSetup.InitializeAndEnsureDatabasesAreCreated(host);
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host
                .CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                    .UseStartup<Startup>()
                    .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    // logging.AddAzureWebAppDiagnostics(); add this if deploying to Azure
                });
            });

            return hostBuilder;
        }

    }
}
