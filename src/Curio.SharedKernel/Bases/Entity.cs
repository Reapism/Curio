using System;
using System.Collections.Generic;
using Curio.SharedKernel.Interfaces;

namespace Curio.SharedKernel.Bases
{
    /// <summary>
    /// A base entity with an identifier and events.
    /// </summary>
    public abstract class Entity
    {
        public virtual Guid Id { get; protected set; }

        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();

    }
}