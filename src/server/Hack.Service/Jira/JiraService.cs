using Hack.Domain;
using Nensure;
using Newtonsoft.Json;
using System;
using System.Net;
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
            Ensure.NotNull(request);
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
                        StatusCode = response.StatusCode
                    };
                }

                var result = JsonConvert.DeserializeObject<GetProjectsResponse>(await response.Content.ReadAsStringAsync());
                result.StatusCode = HttpStatusCode.OK;
                return result;
            }
        }
    }
}