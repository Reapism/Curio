using System.Threading.Tasks;
using Curio.ApplicationCore.Interfaces;
using Curio.SharedKernel;
using Curio.SharedKernel.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Curio.WebApi.Filters
{
    //TODO Ensure ActionResult<TApiResponse<T>> returns properly via httpcode.
    public class HandleApiResponseFilter : IAsyncPageFilter
    {
        private readonly IAppLogger<HandleApiResponseFilter> logger;
        private readonly IWebHostEnvironment environment;

        public HandleApiResponseFilter(IAppLogger<HandleApiResponseFilter> logger, IWebHostEnvironment environment)
        {
            this.logger = logger;
            this.environment = environment;
        }

        public Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            if (environment.EnvironmentName == EnvironmentConstants.Production)
            {
                var isApiResponse = context.Result is ApiResponse apiResponse;
                if (isApiResponse)
                {
                    apiResponse = (ApiResponse)context.Result;
                    apiResponse.ClearException();
                }
            }

            return Task.CompletedTask;
        }

        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            logger.LogInformation($"{context.ActionDescriptor.DisplayName} is executed.");

            return Task.CompletedTask;
        }
    }
}
