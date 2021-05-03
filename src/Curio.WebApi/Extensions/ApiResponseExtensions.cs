using Curio.SharedKernel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Curio.WebApi.Extensions
{
    public static class ApiResponseExtensions
    {
        // TODO ApiResponse should have much more properties to tell the user if its validation thats occuring
        // If successful can also be a warning, if failure is an api failure, etc.
        // Using all these new properties, we can wrap this in a corresponding action result.

        // HttpStatusCode might be it. Based on the range of it, choose which hard wrapped result to return
        public static IActionResult FromApiResponse<T>(this ApiResponse<T> apiResponse)
        {
            switch (apiResponse.HttpStatusCode)
            {
                case StatusCodes.Status200OK:
                    return new OkObjectResult(apiResponse);
                case StatusCodes.Status201Created:
                    return new CreatedResult("", apiResponse);
                case StatusCodes.
            }
        }
    }
}
