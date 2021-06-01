using System.Net.Http;
using Curio.Web.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Curio.Web.Controllers
{
    public class HomeController : WebControllerBase
    {
        private readonly ILogger<HomeController> logger;
        private readonly HttpClient httpClient;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
            : base(httpClientFactory)
        {
            this.logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
