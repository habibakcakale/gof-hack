using Hack.Domain;

namespace Hack.Data
{
    public sealed class WorkItemRepo : EfRepo<WorkItem>, IWorkItemRepo
    {
        public WorkItemRepo(IContextFactory factory) : base(factory)
        {
        }
    }
}