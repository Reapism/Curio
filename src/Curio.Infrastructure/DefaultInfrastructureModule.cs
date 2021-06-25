using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Curio.ApplicationCore.Interfaces;
using Curio.Core.Exceptions;
using Curio.Infrastructure.DomainEvents;
using Curio.Infrastructure.Logging;
using Curio.Infrastructure.Repository;
using Curio.Infrastructure.Services;
using Curio.Infrastructure.Services.Identity;
using Curio.Persistence.Identity;
using Curio.SharedKernel.Interfaces;
using Curio.WebApi.Exchanges.Identity;
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
            RegisterCore(builder);
            RegisterInfrastructure(builder);
        }

        private void RegisterInfrastructure(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EfRepository<>))
                   .As(typeof(IRepository<>))
                   .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository>()
                   .As<IRepository>()
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

            RegisterJsonServices(builder);
            RegisterEmailServices(builder);
            RegisterIdentityServices(builder);
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

        private void RegisterCore(ContainerBuilder builder)
        {

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
