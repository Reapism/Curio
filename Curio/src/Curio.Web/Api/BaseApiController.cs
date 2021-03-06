using Microsoft.AspNetCore.Mvc;

namespace Curio.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController : Controller
    {
    }
}
