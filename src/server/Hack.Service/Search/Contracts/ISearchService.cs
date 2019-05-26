namespace Hack.Service.Search
{
    public interface ISearchService
    {
        SearchResult Predict(SearchRequest request);
    }
}