using System.Collections.Generic;
using Curio.SharedKernel.Interfaces;

namespace Curio.SharedKernel.Bases
{
    public abstract class ValidationResponse : IValidationResponse
    {
        public bool IsValidationsFriendly { get; set; }
        public bool IsFailure { get; set; }
        public IDictionary<string, string> FriendlyValidationMapping { get; set; } = new Dictionary<string, string>();
    }
}
