using Hack.Service;
using Hack.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Nensure;
using System.Linq;
using System.Threading.Tasks;

namespace Hack.Web
{
    public sealed class JiraController : HackController
    {
        private readonly IJiraService _jiraService;
        private readonly IUserService _userService;
        private readonly IProjectService _projectService;

        public JiraController(IJiraService jiraService, IUserService userService, IProjectService projectService)
        {
            Ensure.NotNull(jiraService, userService, projectService);
            _jiraService = jiraService;
            _userService = userService;
            _projectService = projectService;
        }

        [HttpPost("projects")]
        public async Task<GetProjectsResponse> GetProjects(GetProjectsRequest request)
        {
            Ensure.NotNull(request);
            var user = _userService.Get(GetUserId());
            return await _jiraService.GetProjects(request);
        }

        [HttpPost("newProjects")]
        public async Task<GetProjectsResponse> GetNewProjects(GetProjectsRequest request)
        {
            Ensure.NotNull(request);
            var user = _userService.Get(GetUserId());
            var response = await _jiraService.GetProjects(request);
            var dbProjects = _projectService.GetAll();
            var newProjects = response.Values.Where(p => !dbProjects.Any(dp => dp.JiraId != p.Id)).ToArray();
            return new GetProjectsResponse
            {
                Values = newProjects
            };
        }

        [HttpPost("tasks")]
        public async Task<GetTasksResponse> GetTasks(GetTasksRequest request)
        {
            Ensure.NotNull(request);
            var user = _userService.Get(GetUserId());
            return await _jiraService.GetTasks(request);
        }

        [HttpPost("updateEstimate")]
        public async Task<SuccessResponse> UpdateEstimate(UpdateEstimateRequest request)
        {
            Ensure.NotNull(request);
            await _jiraService.UpdateOriginalEstimate(request);
            return new SuccessResponse();
        }
    }
}