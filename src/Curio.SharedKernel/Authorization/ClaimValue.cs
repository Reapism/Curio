using System;
using System.Collections.Generic;
using System.Text;

namespace Curio.SharedKernel.Authorization
{
    public class ClaimValue
    {
        public ClaimValue()
        {

        }

        public ClaimValue(string type, string value)
        {
            Type = type;
            Value = value;
        }

        public string Type { get; }
        public string Value { get; }
    }
}
