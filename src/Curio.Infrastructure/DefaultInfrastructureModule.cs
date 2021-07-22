using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Curio.ApplicationCore.Interfaces;
using Curio.Domain.Exceptions;
using Curio.Infrastructure.DomainEvents;
using Curio.Infrastructure.Logging;
using Curio.Infrastructure.Repository;
using Curio.Infrastructure.Services;
using Curio.Infrastructure.Services.Identity;
using Curio.Persistence.Identity;
using Curio.SharedKernel.Interfaces;
using Curio.WebApi.Exchanges.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Module = Autofac.Module;

namespace Curio.Infrastructure
{
    public class DefaultInfrastructureModule : Module
    {
        private bool isDevelopment = false;
        private Queue<Assembly> assemblies = new();

        /// <summary>
        /// Registers all core and infrastructure components.
        /// </summary>
        /// <param name="isDevelopment"></param>
        /// <param name="callingAssembly"></param>
        public DefaultInfrastructureModule(bool isDevelopment, Assembly callingAssembly = null)
        {
            this.isDevelopment = isDevelopment;
            AddAssemblies();

            if (callingAssembly != null)
            {
                assemblies.Enqueue(callingAssembly);
            }
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (isDevelopment)
            {
                RegisterDevelopmentOnlyDependencies(builder);
            }
            else
            {
                RegisterProductionOnlyDependencies(builder);
            }
            RegisterCommonDependencies(builder);
        }

        private void AddAssemblies()
        {
            var coreAssembly = Assembly.GetAssembly(typeof(CurioException));
            var infrastructureAssembly = Assembly.GetAssembly(typeof(EfRepository));
            var exchangesAssembly = Assembly.GetAssembly(typeof(RegistrationResponse));
            var persistenceAssembly = Assembly.GetAssembly(typeof(CurioIdentityDbContext));

            assemblies.Enqueue(coreAssembly);
            assemblies.Enqueue(infrastructureAssembly);
            assemblies.Enqueue(exchangesAssembly);
            assemblies.Enqueue(persistenceAssembly);
        }

        private void RegisterCommonDependencies(ContainerBuilder builder)
        {
            // Register assembly types for IHandle
            builder.RegisterAssemblyTypes(assemblies.ToArray())
                   .AsClosedTypesOf(typeof(IHandle<>));

            RegisterSharedKernel(builder);
            RegisterExchanges(builder);
            RegisterApplicationCore(builder);
            RegisterInfrastructure(builder);
            RegisterWebApi(builder);
        }

        private void RegisterWebApi(ContainerBuilder builder)
        {
            RegisterRequestsAndRequestHandlers(builder);
        }

        private void RegisterRequestsAndRequestHandlers(ContainerBuilder builder)
        {
            //TODO this does not register the request and handlers properly
            // typeof(IRequest<>).Assembly is MediatR and not web exchanges and web api proj
            // Because this infra project doesn't know about it.
            // Need to create a new autofac module for web api request/response registrations.
        }

        private void RegisterInfrastructure(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EfRepository<>))
                   .As(typeof(IRepository<>))
                   .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository>()
                   .As<IRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EfReadOnlyRepository<>))
                   .As(typeof(IReadOnlyRepository<>))
                   .InstancePerLifetimeScope();

            builder.RegisterType<EfReadOnlyRepository>()
                   .As<IReadOnlyRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType(typeof(LoggerFactory))
                   .As(typeof(ILoggerFactory))
                   .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(LoggerAdapter<>))
                   .As(typeof(IAppLogger<>))
                   .InstancePerLifetimeScope();

            builder.RegisterType<DomainEventDispatcher>()
                   .As<IDomainEventDispatcher>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<SessionUser>()
                   .As<ISessionUser>()
                   .InstancePerLifetimeScope();

            //RegisterAspNetIdentityServices(builder);
            RegisterIdentityServices(builder);
            RegisterEmailServices(builder);
            RegisterJsonServices(builder);
        }

        private static void RegisterJsonServices(ContainerBuilder builder)
        {
            builder.RegisterType<JsonSerializerService>()
                   .As<IJsonSerializer>()
                   .SingleInstance();
        }

        private static void RegisterEmailServices(ContainerBuilder builder)
        {
            builder.RegisterType<EmailSender>()
                   .As<IEmailSender>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<MimeMessageBuilder>()
                   .As<IMimeMessageBuilder>()
                   .InstancePerLifetimeScope();
        }

        private void RegisterAspNetIdentityServices(ContainerBuilder builder)
        {
            // Taken from ASP.NET CORE SRC.
            // https://github.com/dotnet/aspnetcore/blob/v5.0.8/src/Identity/Core/src/IdentityServiceCollectionExtensions.cs
            builder.RegisterType<UserValidator<ApplicationUser>>()
                   .As<IUserValidator<ApplicationUser>>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationUserStore>()
                   .As<UserStore<ApplicationUser, ApplicationRole, CurioIdentityDbContext, Guid>>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<PasswordValidator<ApplicationUser>>()
                   .As<IPasswordValidator<ApplicationUser>>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<PasswordHasher<ApplicationUser>>()
                   .As<IPasswordHasher<ApplicationUser>>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<UpperInvariantLookupNormalizer>()
                   .As<ILookupNormalizer>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<RoleValidator<ApplicationRole>>()
                   .As<IRoleValidator<ApplicationRole>>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<RoleValidator<ApplicationRole>>()
                   .As<IRoleValidator<ApplicationRole>>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<IdentityErrorDescriber>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<SecurityStampValidator<ApplicationRole>>()
                   .As<ISecurityStampValidator>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<TwoFactorSecurityStampValidator<ApplicationRole>>()
                   .As<ITwoFactorSecurityStampValidator>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>>()
                   .As<IUserClaimsPrincipalFactory<ApplicationUser>>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<DefaultUserConfirmation<ApplicationUser>>()
                   .As<IUserConfirmation<ApplicationUser>>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<UserManager<ApplicationUser>>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<SignInManager<ApplicationUser>>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<RoleManager<ApplicationRole>>()
                   .InstancePerLifetimeScope();

        }

        private void RegisterIdentityServices(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(UserRegistrationService<>))
                   .As(typeof(IUserRegistrationService<>))
                   .InstancePerLifetimeScope();

            builder.RegisterType<LoginService>()
                   .As<ILoginService>()
                   .InstancePerLifetimeScope();

        }

        private void RegisterSharedKernel(ContainerBuilder builder)
        {

        }

        private void RegisterDomain(ContainerBuilder builder)
        {

        }

        private void RegisterApplicationCore(ContainerBuilder builder)
        {
            // Replace later with Sha512 if needed.
            builder.RegisterType<Sha256HashingService>()
                   .As<IHashingService>()
                   .InstancePerLifetimeScope();
        }

        private void RegisterExchanges(ContainerBuilder builder)
        {

        }

        private void RegisterDevelopmentOnlyDependencies(ContainerBuilder builder)
        {
            // TODO: Add development only services
        }

        private void RegisterProductionOnlyDependencies(ContainerBuilder builder)
        {
            // TODO: Add production only services
        }

    }
}
