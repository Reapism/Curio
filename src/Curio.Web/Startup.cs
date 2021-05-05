using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Ardalis.ListStartupServices;
using Autofac;
using Curio.Infrastructure;
using Curio.Infrastructure.Data;
using Curio.Infrastructure.Identity;
using Curio.SharedKernel.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Curio.Web
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration config, IWebHostEnvironment env)
        {
            Configuration = config;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Used only for single server, once expanding app, change this.
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.IsEssential = true;
                options.Cookie.HttpOnly = true;
                options.IdleTimeout = TimeSpan.FromSeconds(10);
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            string curioClientConnectionString = Configuration.GetConnectionString("CurioClient");
            string curioIdentityConnectionString = Configuration.GetConnectionString("CurioIdentity");

            StartupSetup.AddDbContext<CurioClientDbContext>(services, curioClientConnectionString);

            AddIdentity(services);
            // not sure if this is needed.
            //StartupSetup.AddDbContext<CurioIdentityDbContext>(services, curioIdentityConnectionString);

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Curio API", Version = "v1" });
                c.EnableAnnotations();
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
            services.Configure<ServiceConfig>(config =>
            {
                config.Services = new List<ServiceDescriptor>(services);

                // optional - default path to view services is /listallservices - recommended to choose your own path
                config.Path = "/listallservices";
            });
        }

        private void AddIdentity(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                    .AddEntityFrameworkStores<CurioIdentityDbContext>()
                    .AddUserStore<ApplicationUserStore>()
                    .AddRoleStore<ApplicationRole>()
                    .AddDefaultTokenProviders();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new DefaultInfrastructureModule(_env.EnvironmentName == EnvironmentConstants.Development));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var isDevelopment = env.EnvironmentName == EnvironmentConstants.Development;

            if (isDevelopment)
            {
                app.UseDeveloperExceptionPage();
                app.UseShowAllServicesMiddleware(); // /listallservices
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseRouting();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Curio API V1"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}
