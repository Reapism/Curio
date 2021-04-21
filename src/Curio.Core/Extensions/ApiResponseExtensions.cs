using System;
using Curio.SharedKernel;
using Curio.SharedKernel.Interfaces;

namespace Curio.Core.Extensions
{
    public static class ApiResponseExtensions
    {
        public static ApiResponse<T> AsSuccessfulApiResponse<T>(this T response, string message = "")
            where T : class
        {
            bool hasResponse = response.Equals(default(T));

            var apiResponse = new ApiResponse<T>
            {
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

            var apiResponse = new ApiResponse<T>(ex)
            {
                Message = message,
                Response = hasResponse ? response : null,
                IsSuccessful = true
            };

            return apiResponse;
        }

        public static ApiResponse<T> AsFailedApiValidationResponse<T>(this T validationResponse, Exception ex = null, string message = "")
            where T : class, IValidationResponse
        {
            bool hasResponse = (bool)(validationResponse?.Equals(default(T)));

            var apiResponse = new ApiResponse<T>(ex)
            {
                Message = message,
                Response = hasResponse ? validationResponse : null,
                IsSuccessful = true
            };

            return apiResponse;
        }
    }
}
