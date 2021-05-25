using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Curio.SharedKernel.Bases;
using Curio.SharedKernel.Interfaces;

namespace Curio.SharedKernel.Bases
{
    public static class GuardValidationClauseExtensions
    {

        public static void Ssn(this IGuardValidationClause guard, string value, [CallerMemberName] string parameterName = "")
        {
            Guard.Against.NullOrWhiteSpace(value, parameterName);

            string regex = @"^(\d{3}-?\d{2}-?\d{4}|XXX-XX-XXXX)$";
            var match = Regex.Match(value, regex);
            if (match.Success)
                throw new ArgumentException("The following input is not a valid SSN.", parameterName);
        }

        public static IValidationResponse Email(this IGuardValidationClause guard, string value, string parameterName)
        {
            Guard.Against.NullOrWhiteSpace(value, parameterName);

            return null;
        }

        public static IValidationResponse Phone(this IGuardValidationClause guard, string value, string countryCode, string parameterName)
        {
            Guard.Against.NullOrWhiteSpace(value, parameterName);
            Guard.Against.NullOrWhiteSpace(countryCode, nameof(countryCode));

            return null;
        }

        public static IValidationResponse BlacklistedWords(this IGuardValidationClause guard, string value, string parameterName)
        {
            Guard.Against.NullOrWhiteSpace(value, parameterName);

            return null;
        }

        public static IValidationResponse BadWords(this IGuardValidationClause guard, string value, string parameterName)
        {
            Guard.Against.NullOrWhiteSpace(value, parameterName);

            return null;
        }

        public static IValidationResponse ChildrenAge(this IGuardValidationClause guard, string value, string parameterName)
        {
            Guard.Against.NullOrWhiteSpace(value, parameterName);

            return null;
        }

        public static IValidationResponse Address(this IGuardValidationClause guard, string value, string parameterName)
        {
            Guard.Against.NullOrWhiteSpace(value, parameterName);

            return null;
        }

        public static IValidationResponse City(this IGuardValidationClause guard, string value, string parameterName)
        {
            Guard.Against.NullOrWhiteSpace(value, parameterName);

            return null;
        }
    }
}
