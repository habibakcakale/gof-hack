using Hack.Data;
using Hack.Domain;
using Nensure;
using System.Collections.Generic;

namespace Hack.Service
{
    public sealed class ProjectService : IProjectService
    {
        private readonly IProjectRepo _repo;

        public ProjectService(IProjectRepo repo)
        {
            Ensure.NotNull(repo);
            _repo = repo;
        }

        public IEnumerable<Project> GetAll()
        {
            return _repo.Get();
        }
    }
}