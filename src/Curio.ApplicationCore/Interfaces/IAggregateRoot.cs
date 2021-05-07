namespace Curio.ApplicationCore.Interfaces
{
    /// <summary>
    /// Marks a particular entity as being the aggregate root.
    /// <para>This entity will have no requirements, can be 
    /// created regardless or independent of other aggregates.</para>
    /// </summary>
    public interface IAggregateRoot
    {
    }
}
