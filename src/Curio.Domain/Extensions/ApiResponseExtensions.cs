using System;
using System.Collections.Generic;
using System.Linq;
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

            var apiResponse = new ApiResponse<T>(message, httpStatusCode: 200)
            {
                Response = hasResponse ? response : null,
                IsSuccessful = true,
            };

            return apiResponse;
        }

        public static ApiResponse<T> AsFailedApiResponse<T>(this T response, string message = "", Exception ex = null)
            where T : class
        {
            bool hasResponse = response.Equals(default(T));

            var apiResponse = new ApiResponse<T>(message, ex, 400)
            {
                Response = hasResponse ? response : null,
                IsSuccessful = false,
            };

            return apiResponse;
        }

        public static ApiResponse<T> AsApiResponse<T>(this T validationResponse, string message = "", Exception ex = null)
            where T : class, IValidationResponse
        {
            // If validation response is not default.
            bool hasResponse = !(bool)(validationResponse?.Equals(default(T)));
            var apiResponse = new ApiResponse<T>(message, ex, 400)
            {
                Response = hasResponse ? validationResponse : null,
                IsSuccessful = false
            };

            return apiResponse;
        }

        public static ApiResponse<T> AsApiResponse<T>(this T validationResponse, ApiResponse fromApiResponse)
            where T : class, IValidationResponse
        {
            // If validation response is not default.
            bool hasResponse = !(bool)(validationResponse?.Equals(default(T)));
            var apiResponse = new ApiResponse<T>(fromApiResponse.Message, null, 400)
            {
                Response = hasResponse ? validationResponse : null,
                IsSuccessful = fromApiResponse.IsSuccessful
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
        /// <param name="message"></param>
        /// <returns></returns>
        public static ApiResponse<V> AsApiResponse<T, V>(this T validationResponse, Exception ex = null, string message = "")
            where T : class, IValidationResponse
            where V : class, IValidationResponse
        {
            // If validation response is not default.
            bool hasResponse = !(bool)(validationResponse?.Equals(default(T)));
            var value = default(V);

            var apiResponse = new ApiResponse<V>(message, ex, 400)
            {
                Response = value,
                IsSuccessful = false
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
        public static ApiResponse<T> AsApiResponse<T>(IDictionary<string, string> validationToTipsMapping, string optionalMessage = "")
            where T : class, IValidationResponse, new()
        {
            var validationResponse = default(T);

            var hasValidations = validationToTipsMapping?.Any() ?? false;

            validationResponse.IsFailure = hasValidations;
            validationResponse.FriendlyValidationMapping = validationToTipsMapping;

            var apiResponse = new ApiResponse<T>(optionalMessage, httpStatusCode: 400)
            {
                Response = validationResponse,
                IsSuccessful = !validationResponse.IsFailure
            };

            return apiResponse;
        }
    }
}
