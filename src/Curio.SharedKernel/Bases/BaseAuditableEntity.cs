using System;
using Curio.SharedKernel.Interfaces;

namespace Curio.SharedKernel.Bases
{
    public abstract class BaseAuditableEntity : BaseEntity, IAuditableEntity
    {
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedByUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedByUser { get; set; }
    }
}
