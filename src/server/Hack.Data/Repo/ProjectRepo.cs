using Hack.Domain;

namespace Hack.Data
{
    public sealed class ProjectRepo : EfRepo<Project>, IProjectRepo
    {
        public ProjectRepo(IContextFactory factory) : base(factory)
        {
        }
    }
}