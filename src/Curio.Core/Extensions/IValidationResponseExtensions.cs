using System.Collections.Generic;

namespace Curio.Core.Extensions
{
    public static class IValidationResponseExtensions
    {
        public static IDictionary<string, string> ToValidationResponse(this (string, string) validationTipTuple)
        {
            return new Dictionary<string, string>()
            {
                { validationTipTuple.Item1, validationTipTuple.Item2 }
            };
        }
    }
}
