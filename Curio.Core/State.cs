using System;
using System.Collections.Generic;
using System.Text;

namespace Curio.Core
{
    public interface IState
    {
        public object Object { get; set; }
        public bool InMemory { get; set; }
        public bool  { get; set; }
    }

    public class State : IState
    {
    }
}
