using Hack.Data;
using Hack.Domain;
using Nensure;
using System.Collections.Generic;
using System.Linq;

namespace Hack.Service
{
    public sealed class WorkItemService : IWorkItemService
    {
        private readonly IWorkItemRepo _repo;

        public WorkItemService(IWorkItemRepo repo)
        {
            Ensure.NotNull(repo);
            _repo = repo;
        }

        public void Create(int projectId, IEnumerable<ProceedRequest.Issue> issues)
        {
            if (issues is null) { return; }

            foreach (var issue in issues)
            {
                _repo.Create(new WorkItem
                {
                    ProjectId = projectId,
                    JiraTicketId = issue.JiraId,
                    Title = issue.Title,
                    Description = issue.Description,
                    Platform = issue.Platform,
                    Tags = issue.Tags.Select(t => new Tag { Name = t }).ToArray(),
                    Estimate = issue.Estimate,
                    UserLevel = issue.UserLevel,
                    UserRole = issue.UserRole
                });
            }
            _repo.Save();
        }
    }
}