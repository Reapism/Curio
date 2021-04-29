using System.Threading.Tasks;
using Curio.SharedKernel.Bases;
using Curio.SharedKernel.Interfaces;

namespace Curio.UnitTests
{
    /// <summary>
    /// Simulates a IDomainEventDispatcher to always return a completed task.
    /// </summary>
    public class NoOpDomainEventDispatcher : IDomainEventDispatcher
    {
        public Task Dispatch(BaseDomainEvent domainEvent)
        {
            return Task.CompletedTask;
        }
    }
}
