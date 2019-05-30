using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nensure;
using Newtonsoft.Json;

namespace Hack.Web.Controllers
{
    public sealed class TestController : HackController
    {
        private readonly ILogger _logger;

        public TestController(ILogger<TestController> logger)
        {
            Ensure.NotNull(logger);
            _logger = logger;
        }

        [AllowAnonymous, HttpGet]
        public string Test(string param)
        {
            _logger.LogInformation(JsonConvert.SerializeObject(HttpContext.Request.QueryString));
            return $"It works: {param}";
        }
    }
}