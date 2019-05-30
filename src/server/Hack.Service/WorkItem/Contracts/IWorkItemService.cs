using System.Collections.Generic;

namespace Hack.Service
{
    public interface IWorkItemService
    {
        void Create(int projectId, IEnumerable<ProceedRequest.Issue> issues);
    }
}