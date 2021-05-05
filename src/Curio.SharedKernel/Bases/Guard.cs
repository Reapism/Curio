using Curio.SharedKernel.Interfaces;

namespace Curio.SharedKernel.Bases
{
    public class Guard : IGuardClause
    {
        // Entry points to guarding against a particular thing.
        public static IGuardClause Against { get; } = new Guard();

        private Guard()
        { }
    }

    public class ValidationGuard : IGuardValidationClause
    {
        public static IGuardValidationClause Against { get; } = new ValidationGuard();

        private ValidationGuard()
        { }
    }
}
