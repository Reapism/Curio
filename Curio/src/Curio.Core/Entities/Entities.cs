using System;
using System.Collections.Generic;
using Curio.SharedKernel.Bases;

namespace Curio.Core.Entities
{
    public class User : BaseAuditableEntity
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsLocked { get; set; }
        public bool IsSuspended { get; set; }
        public bool IsRegistered { get; set; }
        public bool HasAgreedToEula { get; set; }
        public bool HasAgreedToPrivacyPolicy { get; set; }
        public DateTime PasswordLastChangedDate { get; set; }
        public DateTime SuspensionDate { get; set; }
        public LoginType LoginType { get; set; }

        public UserAddress UserAddress { get; set; }
    }

    public class UserAddress : BaseAuditableEntity
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string PostalCode { get; set; }
        public string AdvertisingId { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }

    public class UserPost : BaseAuditableEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool HasAttachments { get; set; }

        public ICollection<UserPost_ImageAttachment> UserPost_ImageAttachments { get; set; }
    }

    public class UserPost_ImageAttachment : BaseAuditableEntity
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] Data { get; set; }

        public Guid UserPostId { get; set; }
        public UserPost UserPost { get; set; }
    }

    public class Lk_PostalCode : BaseEntity
    {

    }

    public enum LoginType
    {
        Email,
        Phone,
    }
}
