using System.Collections.Generic;
using System.Linq;
using Curio.SharedKernel.Interfaces;

namespace Curio.SharedKernel.Bases
{
    public abstract class ValidationResponse : IValidationResponse
    {
        public bool IsValidationsFriendly { get; set; }
        public bool IsFailure { get => ReasonByErrorMapping?.Any() ?? false; }
        public IDictionary<string, string> ReasonByErrorMapping { get; set; } = new Dictionary<string, string>();
    }
}
