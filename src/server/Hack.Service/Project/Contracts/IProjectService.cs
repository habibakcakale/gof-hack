using Hack.Domain;
using System.Collections.Generic;

namespace Hack.Service
{
    public interface IProjectService
    {
        Project Get(int id);

        IEnumerable<Project> GetAll(int? userId= null);

        void Update(Project project);
        Project Create(Project project);
    }
}