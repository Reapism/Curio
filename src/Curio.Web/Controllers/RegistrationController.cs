using Curio.Domain.Entities;
using Curio.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Curio.Web.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IRepository<User> repository;

        public RegistrationController(IRepository<User> repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return Ok();
        }
    }
}
