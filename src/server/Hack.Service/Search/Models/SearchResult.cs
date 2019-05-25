using System;
using System.Collections.Generic;
using System.Text;

namespace Hack.Service.Search.Models
{
    public class SearchResult
    {
        public ICollection<SearchItem> SearchItems { get; set; }
    }

    public class SearchItem
    {
        public string Title { get; set; }
        public int Id { get; set; }
        public SearchType Type { get; set; }
    }
}
