using System.Threading.Tasks;
using Curio.SharedKernel.Interfaces;
using Curio.SharedKernel;

namespace Curio.UnitTests
{
    public class NoOpDomainEventDispatcher : IDomainEventDispatcher
    {
        public Task Dispatch(BaseDomainEvent domainEvent)
        {
            return Task.CompletedTask;
        }
    }
}
