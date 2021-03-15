using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curio.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Curio.Web.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IRepository repository;

        public RegistrationController(IRepository repository)
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
