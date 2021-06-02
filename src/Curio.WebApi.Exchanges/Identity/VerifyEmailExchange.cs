using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Curio.SharedKernel;
using Curio.SharedKernel.Bases;
using MediatR;

namespace Curio.WebApi.Exchanges.Identity
{
    public class VerifyEmailRequest : IRequest<ApiResponse<VerifyEmailResponse>>
    {
    }

    public class VerifyEmailResponse : ValidationResponse
    {
    }
}
