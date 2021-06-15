using System;

namespace Curio.SharedKernel
{
    public class ApiResponse
    {
        public ApiResponse(string message = "", Exception exception = null, int httpStatusCode = default)
        {
            Exception = exception;
            HttpStatusCode = httpStatusCode;
            Message = message;
        }

        public bool IsSuccessful { get; set; }
        public int HttpStatusCode { get; protected set; }
        public bool HasMessage { get => !string.IsNullOrEmpty(Message); }
        public string Message { get; }
        public Exception Exception { get; private set; }

        public void ClearException() { Exception = null; }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public ApiResponse(string message = "", Exception exception = null, int httpStatusCode = default)
            : base(message, exception, httpStatusCode)
        {
        }

        public T Response { get; set; }
    }
}
