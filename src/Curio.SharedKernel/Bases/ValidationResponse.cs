using System.Collections.Generic;
using Curio.SharedKernel.Interfaces;

namespace Curio.SharedKernel.Bases
{
    public abstract class ValidationResponse : IValidationResponse
    {
        public bool IsFriendlyValidations { get; set; }
        public bool IsFailure { get; set; }
        public IDictionary<string, string> ValidationToTipMapping { get; set; } = new Dictionary<string, string>();
    }
}
