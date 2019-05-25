using Hack.Service;
using Hack.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Nensure;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hack.Web
{
    public sealed class JiraController : HackController
    {
        private readonly IJiraService _jiraService;
        private readonly IUserService _userService;

        public JiraController(IJiraService jiraService, IUserService userService)
        {
            Ensure.NotNull(jiraService, userService);
            _jiraService = jiraService;
            _userService = userService;
        }

        [HttpPost("projects")]
        public async Task<GetProjectsResponse> GetProjects(GetProjectsRequest request)
        {
            Ensure.NotNull(request);
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = _userService.Get(id);
            return await _jiraService.GetProjects(request, user);
        }
    }
}