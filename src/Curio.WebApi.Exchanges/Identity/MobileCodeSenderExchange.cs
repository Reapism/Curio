using Curio.SharedKernel;
using Curio.SharedKernel.Bases;
using MediatR;

namespace Curio.WebApi.Exchanges.Identity
{
    public class MobileCodeSenderRequest : IRequest<ApiResponse<MobileCodeSenderResponse>>
    {
        string CodeToSend { get; set; }
    }

    public class MobileCodeSenderResponse : ValidationResponse
    {
    }
}
