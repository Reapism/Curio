using System;

namespace Curio.SharedKernel.Authorization
{
    public class AuthorizationRoleConstants
    {
        public const string Administrator = "Admin";
        public const string EndUser = "End User";
        public const string Advertiser = "Advertiser";
        public const string Internal = "Internal";
    }

    public class AuthorizationSeededUsers
    {
        public static readonly Tuple<string, string> Admin = Tuple.Create("admin@curio.com", "Curio1!");
        public static readonly Tuple<string, string> EndUser = Tuple.Create("enduser@curio.com", "Curio1!");
        public static readonly Tuple<string, string> Advertiser = Tuple.Create("advertiser@curio.com", "Curio1!");
        public static readonly Tuple<string, string> Internal = Tuple.Create("internal@curio.com", "Curio1!");
    }
}
