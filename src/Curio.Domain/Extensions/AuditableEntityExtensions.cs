using System;
using Curio.SharedKernel.Bases;

namespace Curio.Domain.Extensions
{
    public static class AuditableEntityExtensions
    {
        public static T NewAuditableEntity<T>(this T auditableEntity)
            where T : AuditableEntity
        {
            auditableEntity.CreatedDate = DateTime.Now;
            auditableEntity.LastModifiedDate = DateTime.Now;
            auditableEntity.CreatedByUser = "SYSTEM";
            auditableEntity.LastModifiedByUser = "SYSTEM";

            return auditableEntity;
        }

        public static T UpdateAuditableEntity<T>(this T auditableEntity)
            where T : AuditableEntity
        {
            auditableEntity.LastModifiedDate = DateTime.Now;
            auditableEntity.LastModifiedByUser = "SYSTEM";

            return auditableEntity;
        }
    }
}
