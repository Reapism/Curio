using System;

namespace Curio.SharedKernel.Interfaces
{
    /// <summary>
    /// Represents a polymorphic object.
    /// <para>Allows an instance of the object to represent an owner, creator, delegator</para>
    /// </summary>
    /// <remarks>
    /// Allows an object to be polymorpic by
    /// specifying the name and key of the reference for the relationship
    /// to a type that implements this interface.
    /// </remarks>
    public interface IReferenceable<TKey>
    {
        /// <summary>
        /// The primary key of the reference.
        /// </summary>
        TKey ReferenceId { get; set; }
        /// <summary>
        /// The name of the reference.
        /// </summary>
        /// <remarks>
        /// The name of the reference that is related in someway
        /// to an instance of the implemented type.
        /// </remarks>
        string ReferenceName { get; set; }
    }

    /// <summary>
    /// Represents a dual-polymorphic object.
    /// </summary>
    /// <remarks>
    /// Allows an object to be polymorpic by
    /// specifying the name and key of both the source and 
    /// target references.<code>
    ///       <para><b>UserNotification</b>: A type that implements IDualReferencable</para>
    ///       <para>Target: <b>User</b>: User who receives the notification.</para>
    ///       <para>Source: <b>User</b>: User who created the notification.</para>
    /// </code>
    /// </remarks>
    public interface IDualReferenceable<TKey>
    {
        /// <summary>
        /// The primary key of the target reference.
        /// </summary>
        TKey TargetReferenceId { get; set; }

        /// <summary>
        /// The name of the target reference.
        /// </summary>
        /// <remarks>
        /// The name of the target reference that is related
        /// to an instance of the implemented type by being a target.
        /// </remarks>
        string TargetReferenceName { get; set; }

        /// <summary>
        /// The primary key of the source reference.
        /// </summary>
        TKey SourceReferenceId { get; set; }
        /// <summary>
        /// The name of the source reference.
        /// </summary>
        /// <remarks>
        /// The name of the target reference that is related
        /// to an instance of the implemented type by being a target.
        /// </remarks>
        string SourceReferenceName { get; set; }
    }
}
