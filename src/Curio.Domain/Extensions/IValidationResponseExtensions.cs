using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Curio.SharedKernel.Interfaces;

namespace Curio.Core.Extensions
{
    public static class IValidationResponseExtensions
    {
        /// <summary>
        /// Returns a single string containing all the friendly validations.
        /// </summary>
        /// <param name="validationResponse"></param>
        /// <returns></returns>
        /// <remarks>Error authenticating user; The password is invalid.</remarks>
        public static string Flatten(this IValidationResponse validationResponse)
        {
            var flattenStringBuilder = new StringBuilder(string.Empty);

            foreach (var kvp in validationResponse.ReasonByErrorMapping)
            {
                flattenStringBuilder
                    .Append(kvp.Key)
                    .Append("; ")
                    .Append(kvp.Value)
                    .AppendLine();
            }

            return flattenStringBuilder.ToString();
        }
        public static Tuple<string, string> First(this IValidationResponse validationResponse)
        {
            if (validationResponse.ReasonByErrorMapping.Count > 0)
            {
                return validationResponse.ReasonByErrorMapping.First().AsTuple();
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
