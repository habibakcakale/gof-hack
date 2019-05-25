using Hack.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nensure;

namespace Hack.Web.Controllers
{
    public sealed class UserController : HackController
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        private readonly LoginResponse _loginFailedResponse = new LoginResponse
        {
            FailureMessage = "Username or password is invalid."
        };

        public UserController(IUserService userService, IJwtService jwtService)
        {
            Ensure.NotNull(userService, jwtService);
            _userService = userService;
            _jwtService = jwtService;
        }

        [AllowAnonymous, HttpPost("login")]
        public IObjectResponse<LoginResponse> Login(LoginRequest login)
        {
            Ensure.NotNull(login);
            if (_userService.IsLoginValid(login))
            {
                var user = _userService.Get(login.Username);
                var token = _jwtService.GenerateToken(user);
                return Ok(new LoginResponse { Token = token });
            }
            else
            {
                return BadRequest(_loginFailedResponse);
            }
        }

        [AllowAnonymous, HttpPost("register")]
        public IObjectResponse<RegisterResponse> Register(RegisterRequest request)
        {
            Ensure.NotNull(request);
            var result = _userService.Register(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("jiraCredentials")]
        public SetJiraCredentialsResponse JiraCredentials(SetJiraCredentialsRequest request)
        {
            Ensure.NotNull(request);
            return _userService.SetJiraCredentials(request, GetUserId());
        }
    }
}