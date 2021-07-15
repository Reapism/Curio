using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;
using Ardalis.ListStartupServices;
using Autofac;
using Curio.Infrastructure;
using Curio.Persistence.Client;
using Curio.Persistence.Identity;
using Curio.SharedKernel.Constants;
using Curio.WebApi.Exchanges.Identity;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

            AddDbContexts(services);
            AddIdentity(services);

            services.AddControllersWithViews()
                    .AddJsonOptions(options => GetJsonSerializerOptions());

            services.AddRazorPages();
            services.AddMediatR(typeof(Startup).Assembly, typeof(LoginRequest).Assembly);

            AddHttpClient(services);

            AddSwaggerGen(services);

            // add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
            services.Configure<ServiceConfig>(config =>
            {
                config.Services = new List<ServiceDescriptor>(services);

                // optional - default path to view services is /listallservices - recommended to choose your own path
                config.Path = "/listallservices";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
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

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new DefaultInfrastructureModule(_env.EnvironmentName == EnvironmentConstants.Development));
        }

        private void AddDbContexts(IServiceCollection services)
        {
            string curioClientConnectionString = Configuration.GetConnectionString("CurioClientPostgre");
            string curioIdentityConnectionString = Configuration.GetConnectionString("CurioIdentityPostgre");

            StartupSetup.AddDbContext<CurioClientDbContext>(services, curioClientConnectionString);
            StartupSetup.AddDbContext<CurioIdentityDbContext>(services, curioIdentityConnectionString);
        }

        private JsonSerializerOptions GetJsonSerializerOptions()
        {
            var jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return jsonSerializerOptions;
        }

        private void AddSwaggerGen(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Curio.WebApi",
                    Version = "v1",
                    Description = "The Curio API.",
                    TermsOfService = new Uri("https://github.com/Reapism/Curio/blob/master/README.md"),
                    Contact = new OpenApiContact
                    {
                        Name = "Anthony J",
                        Email = "reapsprgm@gmail.com",
                        Url = new Uri("https://twitter.com/iReapism"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Private License",
                        Url = new Uri("https://github.com/Reapism/Curio/blob/master/LICENSE.txt"),
                    }
                }); ;
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

                c.IncludeXmlComments(GetXmlPath());
            });
        }

        private string GetXmlPath()
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            return xmlPath;
        }

        private static void AddHttpClient(IServiceCollection services)
        {
            services.AddHttpClient("WebApi", (httpClient) =>
            {
                // TODO Later, use configuration in appsettings.json
                var uriBuilder = new UriBuilder("https", "localhost", 44322);
                httpClient.BaseAddress = uriBuilder.Uri;
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
        }

        private void AddIdentity(IServiceCollection services)
        {
            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password = GetPasswordOptions();
                options.User = GetUserOptions();
                options.User.RequireUniqueEmail = true;
                options.Lockout = GetLockoutOptions();
                options.ClaimsIdentity = GetClaimsIdentityOptions();
            })
            .AddEntityFrameworkStores<CurioIdentityDbContext>()
            .AddRoles<ApplicationRole>()
            .AddUserStore<ApplicationUserStore>()
            .AddDefaultTokenProviders();
        }

        private ClaimsIdentityOptions GetClaimsIdentityOptions()
        {
            // TODO Created an Issue here https://github.com/microsoft/referencesource/issues/150
            var claimsIdentityOptions = new ClaimsIdentityOptions
            {
                EmailClaimType = ClaimTypes.Email
            };

            return claimsIdentityOptions;
        }

        private LockoutOptions GetLockoutOptions()
        {
            var lockoutOptions = new LockoutOptions
            {
                AllowedForNewUsers = true,
                MaxFailedAccessAttempts = 5,
                DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5)
            };

            return lockoutOptions;
        }

        private PasswordOptions GetPasswordOptions()
        {
            var passwordOptions = new PasswordOptions()
            {
                RequireNonAlphanumeric = true,
                RequireDigit = true,
                RequiredLength = 8,
                RequireLowercase = true,
                RequireUppercase = true,
                RequiredUniqueChars = 1,
            };
            return passwordOptions;
        }

        private UserOptions GetUserOptions()
        {
            var userOptions = new UserOptions()
            {
                RequireUniqueEmail = true,
            };
            return userOptions;
        }
    }
}
