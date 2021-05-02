using System;

namespace Curio.SharedKernel
{
    public class ApiResponse
    {
        public ApiResponse(Exception exception = null)
        {
            Exception = exception;
        }

        public bool IsSuccessful { get; set; }
        public int HttpStatusCode { get; set; }
        public bool HasMessage { get => !string.IsNullOrEmpty(Message); }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public ApiResponse(Exception exception = null)
        {
            Exception = exception;
        }

        public T Response { get; set; }
    }
}
