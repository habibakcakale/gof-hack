using Hack.Domain;
using System.Collections.Generic;

namespace Hack.Service
{
    public sealed class ProceedRequest
    {
        public int ProjectId { get; set; }
        public IEnumerable<Issue> Issues { get; set; }

        public sealed class Issue
        {
            public string JiraId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Platform { get; set; }
            public int Estimate { get; set; }
            public UserLevel UserLevel { get; set; }
            public UserRole UserRole { get; set; }
            public IEnumerable<string> Tags { get; set; }
        }
    }
}