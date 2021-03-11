using Microsoft.AspNetCore.Mvc;

namespace Curio.Web.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : Controller
    {
    }
}
