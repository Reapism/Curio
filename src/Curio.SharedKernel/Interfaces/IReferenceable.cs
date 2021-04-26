using System;

namespace Curio.SharedKernel.Interfaces
{
    public interface IReferenceable
    {
        Guid ReferenceId { get; set; }
        string ReferenceName { get; set; }
    }
}
