using System.Collections.Generic;

namespace Curio.Api.Exchanges.Bases
{
    public interface IValidationResponse
    {
        bool IsFailure { get; set; }
        IDictionary<string, string> ValidationToTipMapping { get; set; } // "Password must contain a symbol", "For example: the @ in [sAmple@123]"
    }
}
