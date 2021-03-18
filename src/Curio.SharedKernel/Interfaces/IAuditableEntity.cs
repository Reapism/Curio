using System;

namespace Curio.SharedKernel.Interfaces
{
    public interface IAuditableEntity
    {
        DateTime LastModifiedDate { get; set; }
        string LastModifiedByUser { get; set; }
        DateTime CreatedDate { get; set; }
        string CreatedByUser { get; set; }
    }
}