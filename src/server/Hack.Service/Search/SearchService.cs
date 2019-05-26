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
        private readonly IUserRepo _userRepo;
        private readonly IContextFactory _factory;
        private readonly ContentDirectory _contentDirectory;

        public SearchService(IUserRepo userRepo, IContextFactory factory, ContentDirectory contentDirectory)
        {
            _userRepo = userRepo;
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
            if (!string.IsNullOrWhiteSpace(issue.Fields.Assignee?.EmailAddress))
            {
                var user = _userRepo.Get(issue.Fields.Assignee.EmailAddress);
                modelInput.UserRole = (int)user.Role;
                modelInput.UserLevel = (int?)user.Level ?? 0;
            }

            var estimated = new MlContextService(_contentDirectory.Path).CreatePredictionEngine(request.Method)
                .Predict(modelInput);

            using (var context = _factory.Create())
            {
                var workItems = context.Set<WorkItem>().FromSql("EXEC SearchWorkItem {0}",
                     string.Concat(modelInput.Title, " ", modelInput.Description));

                return new SearchResult
                {
                    Estimate = estimated.Estimate,
                    SearchItems = workItems.ToList()
                };

            }
        }
    }
}
