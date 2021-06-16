using System.Collections.Generic;

namespace Curio.SharedKernel.Interfaces
{
    public interface IValidationResponse
    {
        bool IsValidationsFriendly { get; set; }
        bool IsFailure { get; }

        // ValidationMessage / Tip to solve validation.
        IDictionary<string, string> ReasonByErrorMapping { get; set; } // "Cannot create password", "Password must have a symbol. For example: the @ in [sAmple@123]"
    }
}
