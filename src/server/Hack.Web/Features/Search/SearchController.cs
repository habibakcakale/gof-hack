using Hack.Service.Search;
using Hack.Service.Search.Models;

namespace Hack.Web.Features.Search
{
    using Hack.Web.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Nensure;

    public class SearchController : HackController
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            Ensure.NotNull(searchService);
            _searchService = searchService;
        }

        [HttpGet]
        public SearchResult Get(SearchRequest request)
        {
            Ensure.NotNull(request);
            return _searchService.Search(request);
        }
    }
}