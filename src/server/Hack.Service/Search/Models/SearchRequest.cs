using System.Data;

namespace Hack.Service.Search
{
    public class SearchRequest
    {
        public GetTasksResponse.JiraIssueDto Issue { get; set; }
        public RegressionMethod Method { get; set; }
    }
}