using Hack.Domain;
using Hack.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nensure;
using System;
using System.Threading.Tasks;

namespace Hack.Web.Controllers
{
    public sealed class UserController : HackController
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly IJiraService _jiraService;

        private readonly LoginResponse _loginFailedResponse = new LoginResponse
        {
            FailureMessage = "Username or password is invalid."
        };

        public UserController(IUserService userService, IJwtService jwtService, IJiraService jiraService)
        {
            Ensure.NotNull(userService, jwtService, jiraService);
            _userService = userService;
            _jwtService = jwtService;
            _jiraService = jiraService;
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
        public async Task<IObjectResponse<RegisterResponse>> Register(RegisterRequest request)
        {
            Ensure.NotNull(request);
            if (!await _jiraService.UserExists(request.Username))
            {
                return BadRequest(new RegisterResponse { FailureMessage = "User does not exist in Jira." });
            }
            var result = _userService.Register(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("roleLevel")]
        public IObjectResponse<SetRoleLevelResponse> SetRoleLevel(SetRoleLevelRequest request)
        {
            Ensure.NotNull(request);
            var result = _userService.SetRoleLevel(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("get")]
        public IObjectResponse<GetUserResponse> GetUser(GetUserRequest request)
        {
            Ensure.NotNull(request);
            var user = request.Id != null ? _userService.Get(request.Id.Value) : _userService.Get(request.Username);
            return user is null
                ? BadRequest(new GetUserResponse { FailureMessage = "User not found." })
                : Ok(new GetUserResponse
                {
                    Username = user.Username,
                    Level = Enum.GetName(typeof(UserLevel), user.Level),
                    Role = Enum.GetName(typeof(UserRole), user.Role)
                });
        }
    }
}