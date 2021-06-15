using System;
using System.Collections.Generic;
using System.Linq;
using Curio.SharedKernel.Interfaces;

namespace Curio.Core.Extensions
{
    public static class IValidationResponseExtensions
    {
        public static Tuple<string, string> First(this IValidationResponse validationResponse)
        {
            if (validationResponse.FriendlyValidationMapping.Count > 0)
            {
                return validationResponse.FriendlyValidationMapping.First().AsTuple();
            }

            return Tuple.Create<string, string>(null, null);
        }

        public static IDictionary<string, string> ToValidationResponse(this (string, string) validationTipTuple)
        {
            return new Dictionary<string, string>()
            {
                { validationTipTuple.Item1, validationTipTuple.Item2 }
            };
        }
    }
}
