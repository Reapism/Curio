using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Autofac;
using Curio.Infrastructure;
using Curio.Persistence.Client;
using Curio.Persistence.Identity;
using Curio.SharedKernel.Constants;
using Curio.WebApi.Exchanges.Identity;
using Curio.WebApi.Filters;
using Curio.WebApi.Handlers.Identity;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Curio.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            AddIdentity(services);
            AddAuthentication(services);
            AddDbContexts(services);

            services.AddMemoryCache();

            services.AddControllers()
                    .AddJsonOptions(options => GetJsonSerializerOptions());

            services.AddMediatR(e =>
            {
                e.AsScoped();
            },
            typeof(Startup).Assembly,
            typeof(EndUserRegistrationRequest).Assembly,
            typeof(EndUserRegistrationHandler).Assembly);

            AddSwaggerGen(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Curio.WebApi v1"));
            }

            if (env.IsProduction() || env.IsStaging())
            {
                app.UseExceptionHandler("Api/Error");
                app.UseHsts();
            }

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCookiePolicy();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new DefaultInfrastructureModule(Environment.EnvironmentName == EnvironmentConstants.Development));
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

        private void AddAuthentication(IServiceCollection services)
        {
            //TODO: Figure out where to store this token
            // It belongs in environment variables but what file will
            // read it? appSettings.json? 
            var jwtToken = Configuration.GetSection("JwtToken").Value;
            var jwtTokenBytes = Encoding.ASCII.GetBytes(jwtToken);

            services.AddAuthentication(config =>
            {
                config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
  
            })
            .AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = !Environment.IsDevelopment(); // make sure only disable in development
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtTokenBytes),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        }

        private void AddIdentity(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password = GetPasswordOptions();
                options.User = GetUserOptions();
                options.User.RequireUniqueEmail = true;
                options.Lockout = GetLockoutOptions();
                options.ClaimsIdentity = GetClaimsIdentityOptions();
                options.SignIn = GetSignInOptions();
                options.Stores = GetStoresOptions();
            })
            .AddEntityFrameworkStores<CurioIdentityDbContext>()
            .AddUserStore<ApplicationUserStore>()
            .AddDefaultTokenProviders();

            services.AddScoped<IPasswordHasher<ApplicationUser>, PasswordHasher<ApplicationUser>>();
        }

        private StoreOptions GetStoresOptions()
        {
            var storesOptions = new StoreOptions()
            {
                ProtectPersonalData = true // TODO implement IProtectedUserStore
            };

            return storesOptions;
        }

        private SignInOptions GetSignInOptions()
        {
            var signInOptions = new SignInOptions()
            {
                RequireConfirmedAccount = false,
                RequireConfirmedEmail = false,
                RequireConfirmedPhoneNumber = false
            };

            return signInOptions;
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
