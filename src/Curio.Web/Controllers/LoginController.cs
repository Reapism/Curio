using Curio.SharedKernel.Interfaces;
using Curio.Web.Controllers.Base;
using Curio.Web.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace Curio.Web.Controllers
{
    public class LoginController : WebControllerBase
    {
        public LoginController(IRepository repository) : base(repository)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginParameters loginParameters)
        {
            return Ok();
        }
    }
}
