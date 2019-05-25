using System.Threading.Tasks;

namespace Hack.Service
{
    public interface IJiraService
    {
        Task<GetProjectsResponse> GetProjects(GetProjectsRequest request);
    }
}