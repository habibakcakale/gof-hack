using Hack.Domain;
using System.Collections.Generic;

namespace Hack.Service
{
    public interface IProjectService
    {
        Project Get(int id);

        IEnumerable<Project> GetAll();

        void Update(Project project);
    }
}