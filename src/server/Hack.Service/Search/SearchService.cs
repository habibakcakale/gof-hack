using System;
using System.Collections.Generic;
using System.Linq;
using Hack.Data;
using Hack.Domain;
using Hack.Domain.Config;
using Microsoft.EntityFrameworkCore;
using Nensure;

namespace Hack.Service.Search
{
    public class SearchService : ISearchService
    {
        private readonly IContextFactory _factory;
        private readonly ContentDirectory _contentDirectory;

        public SearchService(IContextFactory factory, ContentDirectory contentDirectory)
        {
            _factory = factory;
            _contentDirectory = contentDirectory;
        }

        public SearchResult Predict(SearchRequest request)
        {

            var issue = request.Issue;
            Ensure.NotNull(issue);
            Ensure.NotNull(issue.Fields);
            var modelInput = new ModelInput()
            {
                Description = issue.Fields.Description,
                Title = issue.Fields.Summary
            };
            if (!string.IsNullOrWhiteSpace(issue.Fields.Platform?.Value))
            {
                modelInput.Platform = issue.Fields.Platform.Value;
            }

            modelInput.UserRole = (int)GetValue<UserRole>(issue.Fields.UserRole?.Value);
            modelInput.UserLevel = (int)GetValue<UserLevel>(issue.Fields.UserLevel?.Value);

            var estimated = new MlContextService(_contentDirectory.Path).CreatePredictionEngine(request.Method)
                .Predict(modelInput);
            var textSearch = string.Concat(modelInput.Title, " ", modelInput.Description);
            if (string.IsNullOrWhiteSpace(textSearch))
            {
                return new SearchResult
                {
                    Estimate = estimated.Estimate,
                    SearchItems = new List<WorkItem>()
                };
            }
            using (var context = _factory.Create())
            {
                var workItems = context.Set<WorkItem>().FromSql("EXEC SearchWorkItem {0}", textSearch);

                return new SearchResult
                {
                    Estimate = estimated.Estimate,
                    SearchItems = workItems.ToList()
                };

            }
        }

        private T GetValue<T>(string value)
        {
            return Enum.TryParse(typeof(T), value, out var parsed) ? (T)parsed : default(T);
        }
    }
}
