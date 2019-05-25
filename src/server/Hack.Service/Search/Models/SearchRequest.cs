namespace Hack.Service
{
    public class SearchRequest
    {
        public string Query { get; set; }
        public SearchType Type { get; set; }
    }

    public enum SearchType
    {
        Project,
        Section,
        Phase,
        Epic,
        Requirement
    }
}