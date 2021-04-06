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

        // Not a property because EFC (current db provider) thinks its a DbSet
        // More info
        // The entity type 'BaseDomainEvent' requires a primary key to be defined.
        // If you intended to use a keyless entity type, call 'HasNoKey' in 'OnModelCreating'.
        // For more information on keyless entity types, see https://go.microsoft.com/fwlink/?linkid=2141943.
        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
    }
}