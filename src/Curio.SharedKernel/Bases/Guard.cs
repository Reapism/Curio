using Curio.SharedKernel.Interfaces;

namespace Curio.SharedKernel.Bases
{
    public class Guard : IGuardClause
    {
        public static IGuardClause Against { get; } = new Guard();

        private Guard()
        { }
    }
}
