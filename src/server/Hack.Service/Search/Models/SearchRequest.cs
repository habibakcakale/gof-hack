using System;
using System.Collections.Generic;
using System.Text;

namespace Hack.Service.Search.Models
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
