using System;
using Curio.SharedKernel.Bases;

namespace Curio.WebApi.Exchanges.Home
{
    public class ForgotPasswordRequest
    {
        public string LoginName { get; set; }
        public DateTime RequestDate { get; set; }
        public string OperatingSystem { get; set; }

        // Computed properties on the client side.
        public bool IsEmailLogin { get; set; }
        public bool IsMobileLogin { get; set; }
    }

    public class ForgotPasswordResponse : ValidationResponse
    {
    }
}
