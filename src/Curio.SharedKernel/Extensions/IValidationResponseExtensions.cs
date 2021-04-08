using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Curio.SharedKernel.Bases;
using Curio.SharedKernel.Interfaces;

namespace Curio.SharedKernel.Extensions
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
