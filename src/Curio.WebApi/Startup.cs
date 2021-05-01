using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Curio.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Curio.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AddIdentity(services);

            services.AddMemoryCache();

            AddAuthentication(services);

            services.AddControllers();
            services.AddMediatR(typeof(Startup));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Curio.WebApi", Version = "v1" });
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
            });
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
                //config.RequireHttpsMetadata = false; // make sure only disable in development
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Curio.WebApi v1"));
            }

            if (env.IsProduction() || env.IsStaging())
            {

            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddIdentity(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password = GetPasswordOptions();
                options.User = GetUserOptions();
                options.Lockout = GetLockoutOptions();
                options.ClaimsIdentity = GetClaimsIdentityOptions();
            })
                .AddEntityFrameworkStores<CurioIdentityDbContext>()
                .AddUserStore<UserStore<ApplicationUser, ApplicationRole, CurioIdentityDbContext, Guid>>()
                .AddRoleStore<RoleStore<ApplicationRole, CurioIdentityDbContext, Guid>>()
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
