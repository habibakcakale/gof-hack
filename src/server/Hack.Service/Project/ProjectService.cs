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

        public IEnumerable<Project> GetAll()
        {
            return _repo.Get();
        }

        public void Update(Project project)
        {
            Ensure.NotNull(project);
            _repo.Update(project);
            _repo.Save();
        }
    }
}