using System.Threading.Tasks;
using Curio.SharedKernel.Bases;

namespace Curio.SharedKernel.Interfaces
{
    /// <summary>
    /// Provides a <see cref="Task"/> handler for <see cref="BaseDomainEvent"/>(s)
    /// via a <see cref="Handle(T)"/> contract.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IHandle<in T> where T : BaseDomainEvent
    {
        Task Handle(T domainEvent);
    }
}