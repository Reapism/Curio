using System;
using System.Collections.Generic;
using Curio.SharedKernel;
using Curio.SharedKernel.Interfaces;

namespace Curio.Core.Extensions
{
    public static class ApiResponseExtensions
    {
        // TODO These need to be updated, and many more added to facilate the creation of the
        // HttpStatusCodes internally.
        public static ApiResponse<T> AsSuccessfulApiResponse<T>(this T response, string message = "")
            where T : class
        {
            bool hasResponse = response.Equals(default(T));

            var apiResponse = new ApiResponse<T>
            {
                Message = message,
                Response = hasResponse ? response : null,
                IsSuccessful = true,
                HttpStatusCode = 200
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
                IsSuccessful = true,
                HttpStatusCode = 400
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
                IsSuccessful = true,
                HttpStatusCode = 400
            };

            return apiResponse;
        }

        /// <summary>
        /// Return a failed <see cref="ApiResponse{T}"/> wrapped in a <see cref="IValidationResponse"/>.
        /// <para>This modifies the <paramref name="validationResponse"/> and populates it based
        /// on the <paramref name="validationToTipsMapping"/>.</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validationResponse">The <see cref="IValidationResponse"/> instance.</param>
        /// <param name="validationToTipsMapping">Key: Validation Message, Value: A tip if any, for why or how to fix the validation.</param>
        /// <returns></returns>
        public static ApiResponse<T> AsFailedApiValidationResponse<T>(IDictionary<string, string> validationToTipsMapping, string optionalMessage = "")
            where T : class, IValidationResponse, new()
        {
            var validationResponse = default(T);

            validationResponse.IsFailure = true;
            validationResponse.ValidationToTipMapping = validationToTipsMapping;

            var apiResponse = new ApiResponse<T>()
            {
                Response = validationResponse,
                IsSuccessful = true,
                Message = optionalMessage,
            };

            return apiResponse;
        }
    }
}
