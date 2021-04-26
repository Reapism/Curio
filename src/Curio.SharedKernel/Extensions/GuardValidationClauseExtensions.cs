using System.Runtime.CompilerServices;
using Curio.SharedKernel.Bases;
using Curio.SharedKernel.Interfaces;

namespace Curio.SharedKernel.Extensions
{
    public static class GuardValidationClauseExtensions
    {

        public static IValidationResponse Ssn(this IGuardValidationClause guard, string value, [CallerMemberName] string parameterName = "")
        {
            Guard.Against.NullOrWhiteSpace(value, parameterName);

            return null;
        }

        public static IValidationResponse Email(this IGuardValidationClause guard, string value, string parameterName)
        {
            Guard.Against.NullOrWhiteSpace(value, parameterName);

            return null;
        }

        public static IValidationResponse Phone(this IGuardValidationClause guard, string value, string parameterName)
        {
            Guard.Against.NullOrWhiteSpace(value, parameterName);

            return null;
        }

        public static IValidationResponse InternationalPhone(this IGuardValidationClause guard, string value, string parameterName)
        {
            Guard.Against.NullOrWhiteSpace(value, parameterName);

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
