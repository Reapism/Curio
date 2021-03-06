using System;

namespace Curio.Core
{
    public delegate void Instruction();
    public delegate void InstructionWithState(IState state);

    public abstract class Curio
    {
        protected abstract void Reason();
        protected abstract void Weight();

    }
}
