using System.Collections.Generic;
using System.Linq;
using Curio.SharedKernel.Interfaces;

namespace Curio.SharedKernel.Bases
{
    public class ValidationResponse : IValidationResponse
    {
        public bool IsSuccess { get => !ReasonByErrorMapping?.Any() ?? false; }
        /// <inheritdoc/>
        public IDictionary<string, string> ReasonByErrorMapping { get; set; } = new Dictionary<string, string>(2);
    }
}
