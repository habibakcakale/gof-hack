using Hack.Domain;
using Hack.Service;
using Hack.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Nensure;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Hack.Service.GetProjectStasusResponse;

namespace Hack.Web
{
    public sealed class DashboardController : HackController
    {
        private readonly IJiraService _jiraService;
        private readonly IProjectService _projectService;

        public DashboardController(IJiraService jiraService, IProjectService projectService)
        {
            Ensure.NotNull(jiraService, projectService);
            _jiraService = jiraService;
            _projectService = projectService;
        }

        [HttpPost("projectStatus")]
        public async Task<GetProjectStasusResponse> GetProjectStatus(GetProjectStasusRequest request)
        {
            Ensure.NotNull(request);
            var response = new GetProjectStasusResponse();
            var jiraProjectsResult = await _jiraService.GetProjects(new GetProjectsRequest());
            var dbProjects = _projectService.GetAll();
            if (jiraProjectsResult.IsSuccess)
            {
                response.NewProjects = jiraProjectsResult.Values.Where(p => !dbProjects.Any(dp => dp.JiraId != p.Id))
                    .Select(p => new ProjectDto
                    {
                        JiraId = p.Id,
                        Name = p.Name,
                        State = ProjectState.Initial
                    }).ToArray();
            }
            var converter = new Func<Project, ProjectDto>(p => new ProjectDto
            {
                JiraId = p.JiraId,
                Name = p.Name,
                State = p.State
            });

            response.EstimateProjects = dbProjects.Where(p => p.State == ProjectState.Estimation).Select(converter);
            response.TimeSpentProjects = dbProjects.Where(p => p.State == ProjectState.TimeSpent).Select(converter);
            response.CompletedProjects = dbProjects.Where(p => p.State == ProjectState.Complete).Select(converter);
            return response;
        }
    }
}