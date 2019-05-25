using Hack.Domain;

namespace Hack.Service
{
    public sealed class GetProjectsRequest
    {
        public JiraCredentials Credentials { get; set; }
    }
}