using System.Collections.Generic;

namespace Curio.SharedKernel.Interfaces
{
    public interface IValidationResponse
    {
        bool IsFailure { get; }
        // ValidationMessage / Tip to solve validation.
        /// <summary>
        /// A key/value pair storing errors as keys, and the reason for the error as values.
        /// </summary>
        /// <remarks>This <see cref="IDictionary{TKey, TValue}"/> is instantiated by default.</remarks>
        IDictionary<string, string> ReasonByErrorMapping { get; set; } // "Cannot create password", "Password must have a symbol. For example: the @ in [sAmple@123]"
    }
}
