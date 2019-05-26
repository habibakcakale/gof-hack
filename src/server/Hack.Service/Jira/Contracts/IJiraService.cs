using System.Threading.Tasks;

namespace Hack.Service
{
    public interface IJiraService
    {
        Task<GetProjectsResponse> GetProjects(GetProjectsRequest request);

        Task<GetTasksResponse> GetTasks(GetTasksRequest request);

        Task<bool> ValidateCredentials();

        Task<bool> UserExists(string email);

        Task UpdateOriginalEstimate(UpdateEstimateRequest request);
    }
}