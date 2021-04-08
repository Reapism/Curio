using System.Collections.Generic;

namespace Curio.Api.Exchanges.Bases
{
    public interface IValidationResponse
    {
        bool IsFailure { get; set; }
        IDictionary<string, string> ValidationToTipMapping { get; set; } // "Cannot contain spaces", "Try to not use spaces"

    }
}
