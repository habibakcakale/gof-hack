using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hack.Web.Controllers
{
    [ApiController, Route("api/[controller]")]
    [Authorize]
    public abstract class HackController : ControllerBase
    {
        protected int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        protected IObjectResponse<T> Ok<T>(T value) => new OkObjectResponse<T>(value);

        protected IObjectResponse<T> BadRequest<T>(T value) => new BadRequestObjectResponse<T>(value);
    }
}