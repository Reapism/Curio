using System;

namespace Curio.SharedKernel.Interfaces
{
    /// <summary>
    /// Represents a polymorphic domain object.
    /// </summary>
    /// <remarks>
    /// Allows a domain object to be polymorpic by
    /// specifying the name and key of the reference.
    /// </remarks>
    public interface IReferenceable
    {
        Guid ReferenceId { get; set; }
        string ReferenceName { get; set; }
    }

    /// <summary>
    /// Represents a dual-polymorphic domain object.
    /// </summary>
    /// <remarks>
    /// Allows a domain object to be polymorpic by
    /// specifying the name and key of the source and 
    /// target domain references.
    /// </remarks>
    public interface IDualReferenceable
    {
        Guid TargetReferenceId { get; set; }
        string TargetReferenceName { get; set; }

        Guid SourceReferenceId { get; set; }
        string SourceReferenceName { get; set; }
    }
}
