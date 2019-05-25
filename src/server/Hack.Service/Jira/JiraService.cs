using Hack.Domain;
using Nensure;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Hack.Service
{
    public sealed class JiraService : IJiraService
    {
        private static readonly Uri BaseAddress = new Uri("https://gof-hack.atlassian.net");

        public async Task<GetProjectsResponse> GetProjects(GetProjectsRequest request, User user)
        {
            Ensure.NotNull(request, user);
            Ensure.NotNull(user.Credentials);

            const string endpoint = "rest/api/3/project/search";
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                var headerValue = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{user.Credentials.Username}:{user.Credentials.Token}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", headerValue);
                var response = await client.GetAsync(endpoint);
                if (!response.IsSuccessStatusCode)
                {
                    return new GetProjectsResponse
                    {
                        FailureMessage = $"Request ended with a status code of {response.StatusCode}|"
                    };
                }
                return JsonConvert.DeserializeObject<GetProjectsResponse>(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<GetTasksResponse> GetTasks(GetTasksRequest request, User user)
        {
            Ensure.NotNull(request, user);
            Ensure.NotNull(user.Credentials);

            var endpoint = $"/rest/api/2/search?jql=project={request.ProjectId}&maxResults=1000&fields=";
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                var headerValue = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{user.Credentials.Username}:{user.Credentials.Token}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", headerValue);
                var response = await client.GetAsync(endpoint);
                if (!response.IsSuccessStatusCode)
                {
                    return new GetTasksResponse
                    {
                        FailureMessage = $"Request ended with a status code of {response.StatusCode}|"
                    };
                }
                return JsonConvert.DeserializeObject<GetTasksResponse>(await response.Content.ReadAsStringAsync());
            }
        }
    }
}