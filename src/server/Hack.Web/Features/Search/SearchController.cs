using Hack.Service;
using Hack.Service.Search;
using Hack.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Nensure;

namespace Hack.Web
{
    public class SearchController : HackController
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            Ensure.NotNull(searchService);
            _searchService = searchService;
        }

        [HttpPost]
        public SearchResult Get(SearchRequest request)
        {
            Ensure.NotNull(request);
            return _searchService.Predict(request);
        }
    }
}