using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Curio.Core.Exceptions;
using Curio.Core.Interfaces;
using Curio.Infrastructure.Data;
using Curio.Infrastructure.DomainEvents;
using Curio.SharedKernel.Interfaces;
using Module = Autofac.Module;

namespace Curio.Infrastructure
{
    public class DefaultInfrastructureModule : Module
    {
        private bool isDevelopment = false;
        private List<Assembly> assemblies = new();

        /// <summary>
        /// Registers all core and infrastructure components.
        /// </summary>
        /// <param name="isDevelopment"></param>
        /// <param name="callingAssembly"></param>
        public DefaultInfrastructureModule(bool isDevelopment, Assembly callingAssembly = null)
        {
            this.isDevelopment = isDevelopment;
            var coreAssembly = Assembly.GetAssembly(typeof(CurioException));
            var infrastructureAssembly = Assembly.GetAssembly(typeof(EfRepository));
            assemblies.Add(coreAssembly);
            assemblies.Add(infrastructureAssembly);
            if (callingAssembly != null)
            {
                assemblies.Add(callingAssembly);
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

        private void RegisterCommonDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<DomainEventDispatcher>().As<IDomainEventDispatcher>()
                .InstancePerLifetimeScope();
            builder.RegisterType<EfRepository>().As<IRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assemblies.ToArray())
                .AsClosedTypesOf(typeof(IHandle<>));

            builder.RegisterType<EmailSender>().As<IEmailSender>()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>))
                .InstancePerLifetimeScope();
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
