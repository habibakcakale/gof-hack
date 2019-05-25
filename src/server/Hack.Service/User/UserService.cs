using FluentValidation;
using Hack.Data;
using Hack.Domain;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Nensure;
using System;
using System.Text;

namespace Hack.Service
{
    public sealed class UserService : IUserService
    {
        private readonly IUserRepo _repo;
        private readonly AuthenticationConfig _authConfig;

        private readonly LoginResponse _loginFailedResponse = new LoginResponse
        {
            User = null,
            FailureMessage = "Username or password is invalid."
        };

        public UserService(IUserRepo repo, AuthenticationConfig authConfig)
        {
            Ensure.NotNull(repo, authConfig);
            _repo = repo;
            _authConfig = authConfig;
        }

        public LoginResponse Login(LoginRequest request)
        {
            Ensure.NotNull(request);
            request.ValidateAndThrow(request);

            var user = _repo.Get(request.Username);
            Ensure.NotNull(user);
            var hash = HashSaltPassword(request.Password, request.Username);
            return hash == user.PasswordHashed
                ? new LoginResponse
                {
                    User = user
                }
                : _loginFailedResponse;
        }

        public RegisterResponse Register(RegisterRequest request)
        {
            Ensure.NotNull(request);
            request.ValidateAndThrow(request);

            var hashed = HashSaltPassword(request.Password, request.Username);
            var user = _repo.Get(request.Username);
            if (user != null)
            {
                return new RegisterResponse { FailureMessage = $"Username {request.Username} is taken." };
            }
            user = _repo.Create(new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHashed = hashed
            });
            _repo.Save();
            return new RegisterResponse
            {
                User = user
            };
        }

        private string HashSaltPassword(string password, string saltSource)
        {
            var salt = Encoding.UTF8.GetBytes(saltSource);
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }
    }
}