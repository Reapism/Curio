using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Curio.SharedKernel.Interfaces;

namespace Curio.Domain.Extensions
{
    public static class IValidationResponseExtensions
    {
        /// <summary>
        /// Returns a single string containing all the friendly validations.
        /// </summary>
        /// <param name="validationResponse"></param>
        /// <returns></returns>
        /// <remarks>Error authenticating user; The password is invalid.</remarks>
        public static string Flatten(this IValidationResponse validationResponse, string kvpDelimiter = "; ")
        {
            var flattenStringBuilder = new StringBuilder(string.Empty);

            foreach (var kvp in validationResponse.ReasonByErrorMapping)
            {
                flattenStringBuilder
                    .Append(kvp.Key)
                    .Append(kvpDelimiter)
                    .Append(kvp.Value)
                    .AppendLine();
            }

            return flattenStringBuilder.ToString();
        }

        public static IDictionary<string, string> ToValidationResponse(this (string, string) reasonByErrorTuple)
        {
            return new Dictionary<string, string>()
            {
                { reasonByErrorTuple.Item1, reasonByErrorTuple.Item2 }
            };
        }
    }
}
