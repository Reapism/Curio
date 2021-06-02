using Curio.SharedKernel;
using Curio.SharedKernel.Bases;
using MediatR;

namespace Curio.WebApi.Exchanges.Identity
{
    public class VerifyPhoneNumberRequest : IRequest<ApiResponse<VerifyPhoneNumberResponse>>
    {
        string PhoneNumber { get; set; }
        string CodeToSend { get; set; }
        bool VerifyViaMobile { get; set; }
    }

    public class VerifyPhoneNumberResponse : ValidationResponse
    {
    }
}
