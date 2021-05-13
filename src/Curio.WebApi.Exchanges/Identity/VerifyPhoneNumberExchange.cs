using Curio.SharedKernel.Bases;

namespace Curio.WebApi.Exchanges.Identity
{
    public class VerifyPhoneNumberRequest
    {
        string PhoneNumber { get; set; }
        bool VerifyViaMobile { get; set; }
    }

    public class VerifyPhoneNumberResponse : ValidationResponse
    {
    }
}
