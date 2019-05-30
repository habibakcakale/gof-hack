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

        public Project Get(int id)
        {
            return _repo.Find(id);
        }

        public IEnumerable<Project> GetAll(int? userId = null)
        {
            if (userId.HasValue)
                return _repo.Get(p => p.UserId == userId);
            return _repo.Get();
        }

        public void Update(Project project)
        {
            Ensure.NotNull(project);
            _repo.Update(project);
            _repo.Save();
        }

        public Project Create(Project project)
        {
            Ensure.NotNull(project);
            var result = _repo.Create(project);
            _repo.Save();
            return result;
        }
    }
}