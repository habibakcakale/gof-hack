using Microsoft.AspNetCore.Mvc;

namespace Hack.Web.Controllers
{
    [ApiController, Route("api/[controller]")]
    public abstract class HackController : ControllerBase
    {
    }
}