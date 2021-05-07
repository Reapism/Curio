using System.Threading.Tasks;
using Curio.ApplicationCore.Interfaces;
using Curio.SharedKernel;
using Curio.SharedKernel.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Curio.Api.Filters
{
    //TODO Ensure ActionResult<TApiResponse<T>> returns properly via httpcode.
    public class HandleApiResponse : IAsyncPageFilter
    {
        private readonly IAppLogger<HandleApiResponse> logger;
        private readonly IWebHostEnvironment environment;

        public HandleApiResponse(IAppLogger<HandleApiResponse> logger, IWebHostEnvironment environment)
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
                    apiResponse.Exception = null;
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
