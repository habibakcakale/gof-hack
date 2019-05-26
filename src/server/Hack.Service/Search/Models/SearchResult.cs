using System.Collections.Generic;
using Hack.Domain;

namespace Hack.Service.Search
{
    public class SearchResult
    {
        public ICollection<WorkItem> SearchItems { get; set; }
        public float Estimate { get; set; }
    }
}
