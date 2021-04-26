using Curio.SharedKernel.Interfaces;

namespace Curio.SharedKernel.Bases
{
    public class Guard : IGuardClause, IGuardValidationClause
    {
        public static IGuardClause Against { get; } = new Guard();
        public static IGuardValidationClause AgainstV { get; } = new Guard();

        private Guard()
        { }
    }
}
