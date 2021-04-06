using Curio.Core.Entities;
using Curio.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Curio.Web.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IRepository<ToDoItem> repository;

        public RegistrationController(IRepository<ToDoItem> repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            repository.AddAsync(new ToDoItem());
            return View();
        }

        public IActionResult Register()
        {
            return Ok();
        }
    }
}
