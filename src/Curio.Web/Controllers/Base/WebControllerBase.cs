using System.Net.Http;
using Curio.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Curio.Web.Controllers.Base
{
    [ValidateModel]
    public class WebControllerBase : Controller
    {
        public WebControllerBase(IHttpClientFactory httpClientFactory)
        {
            HttpClient = httpClientFactory.CreateClient("WebApi");
        }

        protected HttpClient HttpClient { get; }
    }
}
