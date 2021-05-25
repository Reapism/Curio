using Curio.SharedKernel.Bases;
using MediatR;

namespace Curio.WebApi.Exchanges.Identity
{
    public class LoginRequest : IRequest<LoginResponse>
    {
        public string LoginName { get; set; }
        public string Password { get; set; }

        // Computed values on client side whether LoginName is mobile or email
        public bool IsEmailLogin { get; set; }
        public bool IsMobileLogin { get; set; }
        // IF using mobile, the country the number is for to help validate the phone number.
        public string CountryCode { get; set; }
        
    }

    public class LoginResponse : ValidationResponse
    {

    }
}
