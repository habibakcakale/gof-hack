using Hack.Domain;
using System.Collections.Generic;

namespace Hack.Service
{
    public interface IProjectService
    {
        IEnumerable<Project> GetAll();
    }
}