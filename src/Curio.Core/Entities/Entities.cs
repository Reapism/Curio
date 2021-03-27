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
        public string AdvertisingId { get; set; }

        public UserProfile UserProfile { get; set; }
    }

    public class UserProfile : BaseAuditableEntity
    {
        public string DisplayName { get; set; }
        public string UniqueHandle { get; set; }
        
        // UserProfile -> User 1:1 (Parent)
        public Guid UserId { get; set; }
        public User User { get; set; }

        // UserProfile -> UserAddress 1:1 (FK)
        public UserAddress UserAddress { get; set; }
        public UserProfileBiography UserProfileBiography { get; set; }

        // UserProfile -> UserPost 0:1 -> M ()
        public ICollection<UserPost> UserPosts { get; set; }
        public ICollection<UserFollowing> UserFollowings { get; set; }
        public ICollection<UserFollower> UserFollowers { get; set; }
    }

    public class UserProfileBiography : BaseAuditableEntity
    {
        public string Description { get; set; }
        public ICollection<UserLink> Links { get; set; }
    }

    public class UserAddress : BaseAuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string PostalCode { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }

    public class UserFollowing : BaseAuditableEntity
    {
        public Guid UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }

    public class UserFollower : BaseAuditableEntity
    {
        public Guid UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }

    public class UserSetting : BaseAuditableEntity
    {
        public string DisplayName { get; set; }
        public string Handle { get; set; }
        public string Bio { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public ICollection<UserLink> UserLinks { get; set; }
    }

    public class UserLink : BaseAuditableEntity
    {
        public string DisplayName { get; set; }
        public string HyperLink { get; set; }

        public Guid UserSettingId { get; set; }
        public UserSetting UserSetting { get; set; }
    }

    public class UserPost : BaseAuditableEntity
    {
        public string Contents { get; set; }
        public bool HasAttachments { get; set; }
        // User -> UserPost 1:1 (Parent)
        public Guid UserId { get; set; }
        public User User { get; set; }

        // UserPost -> UserPost_ImageAttachment 0:1 to M 
        public ICollection<UserPost_ImageAttachment> UserPost_ImageAttachments { get; set; }
        // UserPost -> UserPostReply 0:1 to M 
        public ICollection<UserPostReply> UserPostReplies { get; set; }
        public ICollection<UserLike> UserLikes { get; set; }
    }

    public class UserLike : BaseAuditableEntity
    {
        public Guid UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        public Guid UserPostId { get; set; }
        public UserPost UserPost { get; set; }

        public Guid UserPostReplyId { get; set; }
        public UserPostReply UserPostReply { get; set; }
    }

    public class UserPostReply : BaseAuditableEntity
    {
        public Guid UserPostId { get; set; }
        public UserPost UserPost { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool HasAttachments { get; set; }

        public ICollection<UserPost_ImageAttachment> UserPostReply_ImageAttachments { get; set; }
    }
    // Look into CDN's for retreiving images instead of storing the raw data
    // can be really expensive
    // Best to store where the file is contained and how to retrieve it
    // file path, server name, db name etc.
    public class UserPost_ImageAttachment : BaseAuditableEntity
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] Image { get; set; }

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
