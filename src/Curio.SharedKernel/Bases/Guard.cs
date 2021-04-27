using Curio.SharedKernel.Interfaces;

namespace Curio.SharedKernel.Bases
{
    public class Guard : IGuardClause, IGuardValidationClause
    {
        // Entry points to guarding against a particular thing.
        public static IGuardClause Against { get; } = new Guard();
        public static IGuardValidationClause This { get; } = new Guard();

        private Guard()
        { }
    }
}
