using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Hack.Data;
using Hack.Domain;
using Hack.Service.Search.Models;
using Microsoft.EntityFrameworkCore;

namespace Hack.Service.Search
{
    public class SearchService : ISearchService
    {
        private readonly IEntityRepo<Requirement> _requirementRepo;
        public SearchService(IEntityRepo<Requirement> requirementRepo)
        {
            _requirementRepo = requirementRepo;
        }

        public SearchResult Search(SearchRequest request)
        {
            switch (request.Type)
            {
                case SearchType.Project:
                    break;
                case SearchType.Section:
                    break;
                case SearchType.Phase:
                    break;
                case SearchType.Epic:
                    break;
                case SearchType.Requirement:
                    return GetRequirements(request);
            }
            throw new ArgumentOutOfRangeException();
        }

        private SearchResult GetRequirements(SearchRequest request)
        {
            return null;
            //     $"select TOP 15 R.* from dbo.Requirement R INNER JOIN FREETEXTTABLE(dbo.Requirement, (Title, Description), '{request.Query}') FT ON FT.[Key] = R.Id ORDER BY FT.[Rank]");
            // var a =  new SearchResult()
            // {
            //     SearchItems = result.Select(item => new SearchItem
            //     {
            //         Title = item.Title,
            //         Id = item.Id,
            //         Type = SearchType.Requirement
            //     }).ToList()
            // };
            // return a;
        }
    }

    public interface ISearchService
    {
        SearchResult Search(SearchRequest request);
    }
}
