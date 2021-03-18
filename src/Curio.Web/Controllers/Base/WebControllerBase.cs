using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curio.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Curio.Web.Controllers.Base
{
    public class WebControllerBase : Controller
    {
        public WebControllerBase(IRepository repository)
        {
            Repository = repository;
        }

        protected IRepository Repository { get; }
    }
}
