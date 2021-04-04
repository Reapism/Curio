using Curio.SharedKernel.Interfaces;
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
                var isApiResponse = context.Result is IApiResponse apiResponse;
                if (isApiResponse)
                {
                    apiResponse = (IApiResponse)context.Result;
                    apiResponse.Exception = null;
                }
            }
        }
    }
}
