using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hack.Web.Controllers
{
    [ApiController, Route("api/[controller]")]
    [Authorize]
    public abstract class HackController : ControllerBase
    {
    }
}