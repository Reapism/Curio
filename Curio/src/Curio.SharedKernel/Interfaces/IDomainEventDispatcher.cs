using System.Threading.Tasks;
using Curio.SharedKernel;

namespace Curio.SharedKernel.Interfaces
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(BaseDomainEvent domainEvent);
    }
}