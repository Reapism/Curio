using System.Threading.Tasks;
using Curio.SharedKernel;

namespace Curio.SharedKernel.Interfaces
{
    /// <summary>
    /// Contains a method to dispatch a given event and return a Task.
    /// </summary>
    public interface IDomainEventDispatcher
    {
        Task Dispatch(BaseDomainEvent domainEvent);
    }
}