using System;
using Microsoft.EntityFrameworkCore;

namespace Curio.Data
{
    public class CurioClientContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
    }

    public class UserInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string AdvertisingId { get; set; }
    }

    public class User
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsLocked { get; set; }
        public DateTime PasswordLastChanged { get; set; }
        public bool IsSuspended { get; set; }
        public DateTime SuspentionDate { get; set; }
        public LoginType LoginType { get; set; }
    }

    public enum LoginType
    {
        Email,
        Phone,
    }
}

