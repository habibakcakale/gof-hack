using Hack.Service;
using Hack.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Nensure;
using System.Threading.Tasks;

namespace Hack.Web
{
    public sealed class JiraController : HackController
    {
        private readonly IJiraService _jiraService;

        public JiraController(IJiraService jiraService)
        {
            Ensure.NotNull(jiraService);
            _jiraService = jiraService;
        }

        [HttpPost("projects")]
        public async Task<GetProjectsResponse> GetProjects([FromBody]GetProjectsRequest request)
        {
            Ensure.NotNull(request);
            return await _jiraService.GetProjects(request);
        }
    }
}