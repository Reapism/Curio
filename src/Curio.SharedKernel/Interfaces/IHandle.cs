using System.Threading.Tasks;
using Curio.SharedKernel.Bases;

namespace Curio.SharedKernel.Interfaces
{
    public interface IHandle<in T> where T : BaseDomainEvent
    {
        Task Handle(T domainEvent);
    }
}