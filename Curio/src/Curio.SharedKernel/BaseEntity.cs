using System;
using System.Collections.Generic;

namespace Curio.SharedKernel
{
    /// <summary>
    /// A base entity with an identifier and events.
    /// </summary>
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
    }
}