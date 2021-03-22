using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Curio.Core.Exceptions
{
    /// <summary>
    /// The base class for all Curio
    /// exceptions.
    /// </summary>
    public class CurioException : Exception
    {
        public CurioException(string message)
            : base (message)
        {
        }
        
        public CurioException Flatten()
        {
            var exceptions = new Queue<Exception>();

            while (!(this.InnerException is null))
                exceptions.Enqueue(this.InnerException);

            var delimitedMessage = string.Join(Environment.NewLine, exceptions.Select(e => e.Message));
            var delimitedStackTrace = string.Join($"{Environment.NewLine}{Environment.NewLine}", exceptions.Select(e => e.StackTrace));
            var curioException = new CurioException(delimitedMessage);

            return curioException;

        }
    }
}
