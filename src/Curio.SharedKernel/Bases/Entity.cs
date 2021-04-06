using System;
using System.Collections.Generic;

namespace Curio.SharedKernel.Bases
{
    /// <summary>
    /// A base entity with an identifier and events.
    /// </summary>
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        public List<BaseDomainEvent> Events { get; protected set; } = new List<BaseDomainEvent>();
    }
}