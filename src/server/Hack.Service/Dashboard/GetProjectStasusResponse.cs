using Hack.Domain;
using System.Collections.Generic;

namespace Hack.Service
{
    public sealed class GetProjectStasusResponse
    {
        public IEnumerable<ProjectDto> NewProjects { get; set; }
        public IEnumerable<ProjectDto> EstimateProjects { get; set; }
        public IEnumerable<ProjectDto> TimeSpentProjects { get; set; }
        public IEnumerable<ProjectDto> CompletedProjects { get; set; }

        public sealed class ProjectDto
        {
            public string JiraId { get; set; }
            public string Name { get; set; }
            public string Key { get; set; }
            public ProjectState State { get; set; }
            public int ProjectId { get; set; }
        }
    }
}