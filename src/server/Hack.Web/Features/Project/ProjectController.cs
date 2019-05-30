using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hack.Domain;
using Hack.Service;
using Hack.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Nensure;

namespace Hack.Web
{
    public sealed class ProjectController : HackController
    {
        private readonly IProjectService _projectService;
        private readonly IJiraService _jiraService;
        private readonly IWorkItemService _workItemService;

        public ProjectController(IProjectService projectService, IJiraService jiraService, IWorkItemService workItemService)
        {
            Ensure.NotNull(projectService, workItemService);
            _projectService = projectService;
            _jiraService = jiraService;
            _workItemService = workItemService;
        }

        [HttpPost("proceed")]
        public async Task<IObjectResponse<ProceedResponse>> Proceed(ProceedRequest request)
        {
            Ensure.NotNull(request);
            switch (request.State)
            {
                case ProjectState.Initial:
                    var projects = await _jiraService.GetProjects(new GetProjectsRequest());
                    var jiraProject = projects.Values.FirstOrDefault(item => item.Id == request.JiraId);
                    if (jiraProject != null)
                        _projectService.Create(new Project()
                        {
                            JiraId = jiraProject.Id,
                            Name = jiraProject.Name,
                            UserId = GetUserId(),
                            State = ProjectState.Estimation
                        });
                    break;
                case ProjectState.Estimation:
                    {
                        var project = _projectService.Get(request.ProjectId);
                        if (project is null)
                        {
                            return BadRequest(new ProceedResponse { FailureMessage = "Project not found." });
                        }

                        project.State = ProjectState.TimeSpent;
                        _projectService.Update(project);
                        break;
                    }
                case ProjectState.TimeSpent:
                    {
                        var project = _projectService.Get(request.ProjectId);
                        if (project is null)
                        {
                            return BadRequest(new ProceedResponse { FailureMessage = "Project not found." });
                        }

                        project.State = ProjectState.Complete;
                        var response = await _jiraService.GetTasks(new GetTasksRequest { ProjectId = project.JiraId });
                        var completedIssues = response.Issues.Where(i => i.Fields.Resolution?.name?.ToString() == "Done");
                        _projectService.Update(project);
                        _workItemService.Create(request.ProjectId, completedIssues.Select(i => new ProceedRequest.Issue
                        {
                            JiraId = i.Id,
                            Description = i.Fields.Description,
                            Estimate = (int?)i.Fields.TimeOriginalEstimate ?? 0,
                            Platform = i.Fields.Platform?.Value,
                            Title = i.Fields.Summary,
                            UserLevel = (UserLevel)Enum.Parse(typeof(UserLevel), i.Fields.UserLevel?.Value),
                            UserRole = (UserRole)Enum.Parse(typeof(UserRole), i.Fields.UserRole?.Value),
                        }).ToArray());
                        break;
                    }

                default:
                    return BadRequest(new ProceedResponse { FailureMessage = $"Invalid project state: {request.State}" });
            }
            return Ok(new ProceedResponse());
        }

    }
}