using Hack.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nensure;
using System.Security.Claims;

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
        public LoginResponse Login(LoginRequest login)
        {
            Ensure.NotNull(login);
            if (_userService.IsLoginValid(login))
            {
                var user = _userService.Get(login.Username);
                var token = _jwtService.GenerateToken(user);
                return new LoginResponse { Token = token };
            }
            else
            {
                return _loginFailedResponse;
            }
        }

        [AllowAnonymous, HttpPost("register")]
        public RegisterResponse Register(RegisterRequest request)
        {
            Ensure.NotNull(request);
            return _userService.Register(request);
        }

        [HttpPost("jiraCredential")]
        public SetJiraCredentialsResponse JiraCredentials(SetJiraCredentialsRequest request)
        {
            Ensure.NotNull(request);
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return _userService.SetJiraCredentials(request, userId);
        }
    }
}