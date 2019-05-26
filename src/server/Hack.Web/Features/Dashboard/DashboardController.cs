using Hack.Domain;
using Hack.Service;
using Hack.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Nensure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog.Filters;
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
            var dbProjects = _projectService.GetAll().ToList();
            if (jiraProjectsResult.IsSuccess)
            {
                response.NewProjects = jiraProjectsResult.Values.Where(p => dbProjects.All(dp => dp.JiraId != p.Id))
                    .Select(p => new ProjectDto
                    {
                        JiraId = p.Id,
                        Name = p.Name,
                        State = ProjectState.Initial,
                        Key = p.Key
                    }).ToArray();
            }

            response.EstimateProjects = FilterProjects(dbProjects, jiraProjectsResult.Values, ProjectState.Estimation);
            response.TimeSpentProjects = FilterProjects(dbProjects, jiraProjectsResult.Values, ProjectState.TimeSpent);
            response.CompletedProjects = FilterProjects(dbProjects, jiraProjectsResult.Values, ProjectState.Complete);
            return response;
        }

        private IEnumerable<ProjectDto> FilterProjects(IEnumerable<Project> dbProjects, IEnumerable<GetProjectsResponse.ProjectData> values, ProjectState state)
        {
            return from dbProject in dbProjects
                   join value in values on dbProject.JiraId equals value.Id
                   where dbProject.State == state
                   select new ProjectDto
                   {
                       JiraId = dbProject.JiraId,
                       Name = dbProject.Name,
                       State = dbProject.State,
                       Key = value.Key,
                       ProjectId = dbProject.Id
                   };
        }
    }
}