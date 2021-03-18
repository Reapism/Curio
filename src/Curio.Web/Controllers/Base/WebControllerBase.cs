using Curio.SharedKernel.Interfaces;
using Curio.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Curio.Web.Controllers.Base
{
    [ValidateModel]
    public class WebControllerBase : Controller
    {
        public WebControllerBase(IRepository repository)
        {
            Repository = repository;
        }

        protected IRepository Repository { get; }
    }
}
