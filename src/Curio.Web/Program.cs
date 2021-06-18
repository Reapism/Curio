﻿using System;
using Autofac.Extensions.DependencyInjection;
using Curio.ApplicationCore.Interfaces;
using Curio.Infrastructure.Data;
using Curio.Infrastructure.Identity;
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
                    var logger = services.GetRequiredService<IAppLogger<Program>>();
                    logger.LogError(ex, "The database(s) is not initialized.");
                }
            }
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
