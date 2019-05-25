using Hack.Service;
using Microsoft.AspNetCore.Mvc;
using Nensure;

namespace Hack.Web.Controllers
{
    public sealed class UserController : HackController
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public UserController(IUserService userService, IJwtService jwtService)
        {
            Ensure.NotNull(userService, jwtService);
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public LoginResponse Login(LoginRequest login)
        {
            Ensure.NotNull(login);
            var response = _userService.Login(login);
            if (response.User is null)
            {
                return response;
            }
            response.Token = _jwtService.GenerateToken(response.User);
            return response;
        }

        [HttpPost("register")]
        public RegisterResponse Register(RegisterRequest request)
        {
            Ensure.NotNull(request);
            return _userService.Register(request);
        }
    }
}