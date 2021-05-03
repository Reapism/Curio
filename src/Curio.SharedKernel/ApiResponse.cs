using System;

namespace Curio.SharedKernel
{
    public class ApiResponse
    {
        public ApiResponse(Exception exception = null, int httpStatusCode = default)
        {
            Exception = exception;
            HttpStatusCode = httpStatusCode;
        }

        public bool IsSuccessful { get; set; }
        public int HttpStatusCode { get; protected set; }
        public bool HasMessage { get => !string.IsNullOrEmpty(Message); }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public ApiResponse(Exception exception = null, int httpStatusCode = default)
            : base(exception, httpStatusCode)
        {
        }

        public T Response { get; set; }
    }
}
