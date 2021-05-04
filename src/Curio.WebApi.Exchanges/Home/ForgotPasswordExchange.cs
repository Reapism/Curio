using System;
using Curio.SharedKernel;
using Curio.SharedKernel.Bases;
using MediatR;

namespace Curio.WebApi.Exchanges.Home
{
    public class ForgotPasswordRequest : IRequest<ApiResponse<ForgotPasswordResponse>>
    {
        public string LoginName { get; set; }
        public DateTime RequestDate { get; set; }
        // Device used when choosing to forget the password.
        public string OperatingSystem { get; set; }

        // Computed properties on the client side.
        public bool IsEmailLogin { get; set; }
        public bool IsMobileLogin { get; set; }
    }

    public class ForgotPasswordResponse : ValidationResponse
    {
    }
}
