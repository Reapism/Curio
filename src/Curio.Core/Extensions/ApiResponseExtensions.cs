using System;
using Curio.SharedKernel.Interfaces;

namespace Curio.Core.Extensions
{
    public static class ApiResponseExtensions
    {
        public static ApiResponse<T> AsSuccessfulApiResponse<T>(this T response, string message = "")
            where T : class
        {
            bool hasResponse = response.Equals(default(T));
            bool hasMessage = string.IsNullOrEmpty(message);

            var apiResponse = new ApiResponse<T>
            {
                Exception = null,
                HasMessage = hasMessage,
                Message = message,
                Response = hasResponse ? response : null,
                IsSuccessful = true
            };

            return apiResponse;
        }

        public static ApiResponse<T> AsFailedApiResponse<T>(this T response, Exception ex = null, string message = "")
            where T : class
        {
            bool hasResponse = response.Equals(default(T));
            bool hasMessage = string.IsNullOrEmpty(message);
            bool hasException = ex is not null;

            var apiResponse = new ApiResponse<T>
            {
                Exception = ex,
                HasMessage = hasMessage,
                Message = message,
                Response = hasResponse ? response : null,
                IsSuccessful = true
            };

            return apiResponse;
        }
    }
}
