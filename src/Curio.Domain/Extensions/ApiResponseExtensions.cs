using System;
using System.Collections.Generic;
using System.Linq;
using Curio.SharedKernel;
using Curio.SharedKernel.Interfaces;

namespace Curio.Domain.Extensions
{
    public static class ApiResponseExtensions
    {
        /// <summary>
        /// 200
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <param name="overallMessage"></param>
        /// <returns></returns>
        public static ApiResponse<T> AsOkApiResponse<T>(this T response, string overallMessage = "")
            where T : class
        {
            return AsApiResponse(response, overallMessage, httpStatusCode: 200);
        }

        /// <summary>
        /// 400
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <param name="overallMessage"></param>
        /// <returns></returns>
        public static ApiResponse<T> AsBadRequestApiResponse<T>(this T response, string overallMessage = "", Exception exception = null)
            where T : class
        {
            return AsApiResponse(response, overallMessage, httpStatusCode: 400);
        }

        public static ApiResponse<T> AsUnauthorizedApiResponse<T>(this T response, string overallMessage = "", Exception exception = null)
            where T : class
        {
            return AsApiResponse(response, overallMessage, httpStatusCode: 401);
        }

        public static ApiResponse<T> AsForbidApiResponse<T>(this T response, string overallMessage = "")
            where T : class
        {
            return AsApiResponse(response, overallMessage, httpStatusCode: 403);
        }

        public static ApiResponse<T> AsNotFoundApiResponse<T>(this T response, string overallMessage = "")
            where T : class
        {
            return AsApiResponse(response, overallMessage, httpStatusCode: 404);
        }

        public static ApiResponse<T> AsApiResponse<T>(this T validationResponse, string overallMessage = "", Exception ex = null)
            where T : class, IValidationResponse
        {
            // If validation response is not default (empty).
            bool hasValidationResponse = HasValidationResponse(validationResponse);
            var apiResponse = new ApiResponse<T>(overallMessage, ex, 400)
            {
                Response = hasValidationResponse ? validationResponse : null,
                IsSuccessful = IsSuccessfulForValidationResponse(hasValidationResponse)
            };

            return apiResponse;
        }

        public static ApiResponse<T> FromApiResponse<T>(this T validationResponse, ApiResponse fromApiResponse)
            where T : class, IValidationResponse
        {
            // If validation response is not default.
            bool hasValidationResponse = HasValidationResponse(validationResponse);
            var apiResponse = new ApiResponse<T>(fromApiResponse.Message, null, 400)
            {
                Response = hasValidationResponse ? validationResponse : null,
                IsSuccessful = IsSuccessfulForValidationResponse(hasValidationResponse)
            };

            return apiResponse;
        }

        /// <summary>
        /// Wraps a <typeparamref name="T"/> into a specific <see cref="ApiResponse"/>&lt;<typeparamref name="V"/>&gt;.
        /// </summary>
        /// <remarks>Generates a <see langword="default"/> instance of <typeparamref name="V"/> to wrap with.</remarks>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="validationResponse"></param>
        /// <param name="ex"></param>
        /// <param name="optionalMessage"></param>
        /// <returns></returns>
        public static ApiResponse<V> AsApiResponse<T, V>(this T validationResponse, Exception ex = null, string optionalMessage = "")
            where T : class, IValidationResponse
            where V : class, IValidationResponse
        {
            // If validation response is not default.
            bool hasValidationResponse = HasValidationResponse(validationResponse);
            var response = default(V);
            var apiResponse = new ApiResponse<V>(optionalMessage, ex, 400)
            {
                Response = response,
                IsSuccessful = IsSuccessfulForValidationResponse(hasValidationResponse),
            };

            return apiResponse;
        }

        /// <summary>
        /// Return a failed <see cref="ApiResponse{T}"/> wrapped in a <see cref="IValidationResponse"/>.
        /// <para>This modifies the <paramref name="validationResponse"/> and populates it based
        /// on the <paramref name="reasonsByErrorMapping"/>.</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validationResponse">The <see cref="IValidationResponse"/> instance.</param>
        /// <param name="reasonsByErrorMapping">Key: Error Value: Friendly validation message on why the error occured.</param>
        /// <returns></returns>
        public static ApiResponse<T> AsApiResponse<T>(this IDictionary<string, string> reasonsByErrorMapping, string optionalMessage = "")
            where T : class, IValidationResponse, new()
        {
            var hasValidations = reasonsByErrorMapping?.Any() ?? false;

            var validationResponse = default(T);
            validationResponse.ReasonByErrorMapping = reasonsByErrorMapping;

            return AsApiResponse(validationResponse: validationResponse, overallMessage: optionalMessage);
        }

        private static ApiResponse<T> AsApiResponse<T>(this T response, string message = "", Exception exception = null, int httpStatusCode = 200)
            where T : class
        {
            var hasResponse = HasResponse(response);

            var apiResponse = new ApiResponse<T>(message, exception, httpStatusCode)
            {
                IsSuccessful = IsSuccessStatusCode(httpStatusCode),
                Response = response
            };

            return apiResponse;
        }

        private static bool IsSuccessStatusCode(int httpStatusCode)
        {
            return (httpStatusCode >= 200) && (httpStatusCode <= 299);
        }

        // public for unit testing or use the other public functions to test.
        private static bool HasResponse<T>(this T response)
            where T : class
        {
            var hasResponse = !response?.Equals(default(T)) ?? false;

            return hasResponse;
        }

        private static bool HasValidationResponse<T>(this T validationResponse)
            where T : class, IValidationResponse
        {
            if (validationResponse is null)
                return false;

            if (validationResponse.IsFailure)
                return true;

            if (validationResponse.ReasonByErrorMapping.Any())
                return true;

            return false;
        }

        /// <summary>
        /// If there is a validation response, the response is unsuccessful as there is a validation. 
        /// </summary>
        /// <param name="hasResponse"></param>
        /// <returns></returns>
        private static bool IsSuccessfulForValidationResponse(bool hasValidationResponse)
        {
            return !hasValidationResponse;
        }
    }
}
