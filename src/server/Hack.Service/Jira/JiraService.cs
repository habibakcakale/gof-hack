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
        private readonly JiraCredentialsConfig _credentials;

        private string AuthHeader => Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_credentials.Username}:{_credentials.Token}"));

        public JiraService(JiraCredentialsConfig credentials)
        {
            Ensure.NotNull(credentials);
            _credentials = credentials;
        }

        public async Task<GetProjectsResponse> GetProjects(GetProjectsRequest request)
        {
            Ensure.NotNull(request);

            const string endpoint = "rest/api/3/project/search";
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", AuthHeader);
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

        public async Task<GetTasksResponse> GetTasks(GetTasksRequest request)
        {
            Ensure.NotNull(request);

            var endpoint = $"/rest/api/2/search?jql=project={request.ProjectId}&maxResults=1000&fields=";
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", AuthHeader);
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

        public async Task<bool> ValidateCredentials()
        {
            const string endpoint = "/rest/api/3/mypermissions";
            using (var client = new HttpClient())

            {
                client.BaseAddress = BaseAddress;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", AuthHeader);
                var response = await client.GetAsync(endpoint);
                if (!response.IsSuccessStatusCode) { return false; }
                dynamic result = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                return result.permissions?.ADMINISTER?.type == "GLOBAL";
            }
        }

        public async Task<bool> UserExists(string email)
        {
            Ensure.NotNullOrEmpty(email);
            var endpoint = $"/rest/api/3/user/search?query={email}";
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", AuthHeader);
                var response = await client.GetAsync(endpoint);
                if (!response.IsSuccessStatusCode) { return false; }
                dynamic result = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                return result.Count == 1;
            }
        }

        public async Task UpdateOriginalEstimate(UpdateEstimateRequest request)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", AuthHeader);
                var requestBody = JsonConvert.SerializeObject(new
                {
                    update = new
                    {
                        timetracking = new[]
                        {
                            new
                            {
                                edit = new
                                {
                                    originalEstimate = request.Estimate
                                }
                            }
                        }
                    }
                });
                var response = await client.PutAsync($"/rest/api/2/issue/{request.IssueIdOrKey}",
                       new StringContent(requestBody, Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
            }
        }
    }
}