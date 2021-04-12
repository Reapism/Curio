using Curio.SharedKernel.Bases;

namespace Curio.WebApi.Exchanges.Home
{
    public class LoginRequest
    {
        public string LoginName { get; set; }
        public string Password { get; set; }

        // Compute on client side whether input is mobile or email
        public bool IsEmailLogin { get; set; }
        public bool IsMobileLogin { get; set; }


    }

    public class LoginResponse : ValidationResponse
    {

    }
}
