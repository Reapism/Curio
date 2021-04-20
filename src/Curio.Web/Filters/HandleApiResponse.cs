using Curio.SharedKernel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Curio.Web.Filters
{
    public class HandleApiResponse : ActionFilterAttribute
    {
        private readonly IWebHostEnvironment environment;

        public HandleApiResponse(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (environment.EnvironmentName == "Production")
            {
                var isApiResponse = context.Result is ApiResponse apiResponse;
                if (isApiResponse)
                {
                    apiResponse = (ApiResponse)context.Result;
                    apiResponse.Exception = null;
                }
            }
        }
    }
}
