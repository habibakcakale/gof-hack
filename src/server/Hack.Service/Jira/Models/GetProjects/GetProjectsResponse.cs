using System;
using System.Collections.Generic;

namespace Hack.Service
{
    public sealed class GetProjectsResponse : SuccessResponse
    {
        public IEnumerable<ProjectData> Values { get; set; }

        public sealed class ProjectData
        {
            public Uri Self { get; set; }
            public string Id { get; set; }
            public string Key { get; set; }
            public string Name { get; set; }
        }
    }
}