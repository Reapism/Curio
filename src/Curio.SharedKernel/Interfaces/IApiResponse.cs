using System;

namespace Curio.SharedKernel.Interfaces
{
    public class ApiResponse
    {
        public bool IsSuccessful { get; set; }
        public bool HasMessage { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }

    public class ApiResponse<T>
        where T : class
    {
        public T Response { get; set; }
        public bool IsSuccessful { get; set; }
        public bool HasMessage { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}
