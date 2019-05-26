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
        private readonly IWorkItemService _workItemService;

        public ProjectController(IProjectService projectService, IWorkItemService workItemService)
        {
            Ensure.NotNull(projectService, workItemService);
            _projectService = projectService;
            _workItemService = workItemService;
        }

        [HttpPost("proceed")]
        public IObjectResponse<ProceedResponse> Proceed(ProceedRequest request)
        {
            Ensure.NotNull(request);
            var project = _projectService.Get(request.ProjectId);
            if (project is null) { return BadRequest(new ProceedResponse { FailureMessage = "Project not found." }); }
            switch (project.State)
            {
                case ProjectState.Estimation:
                    project.State = ProjectState.TimeSpent;
                    _projectService.Update(project);
                    break;

                case ProjectState.TimeSpent:
                    project.State = ProjectState.Complete;
                    _projectService.Update(project);
                    _workItemService.Create(request.ProjectId, request.Issues);
                    break;

                default:
                    return BadRequest(new ProceedResponse { FailureMessage = $"Invalid project state: {project.State}" });
            }
            return Ok(new ProceedResponse());
        }
    }
}