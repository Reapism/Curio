using System;

namespace Curio.SharedKernel.Interfaces
{
    /// <summary>
    /// Any Entity must have an <see cref="Id"/>.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// The unique identifier for an entity.
        /// </summary>
        Guid Id { get; set; }
    }
}
