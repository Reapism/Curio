using System.Collections.Generic;

namespace Curio.SharedKernel.Interfaces
{
    public interface IValidationResponse
    {
        bool IsValidationsFriendly { get; set; }
        bool IsFailure { get; set; }

        // ValidationMessage / Tip to solve validation.
        IDictionary<string, string> FriendlyValidationMapping { get; set; } // "Password must contain a symbol", "For example: the @ in [sAmple@123]"
    }
}
