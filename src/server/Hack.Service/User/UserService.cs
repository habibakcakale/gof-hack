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

        public UserService(IUserRepo repo)
        {
            Ensure.NotNull(repo);
            _repo = repo;
        }

        public User Get(int id)
        {
            return _repo.Find(id);
        }

        public User Get(string username)
        {
            return _repo.Get(username);
        }

        public bool IsLoginValid(LoginRequest request)
        {
            Ensure.NotNull(request);
            var user = _repo.Get(request.Username);
            if (user is null) { return false; }
            var hash = HashSaltPassword(request.Password, request.Username);
            return hash == user.PasswordHashed;
        }

        public RegisterResponse Register(RegisterRequest request)
        {
            Ensure.NotNull(request);

            var hashed = HashSaltPassword(request.Password, request.Username);
            var user = _repo.Get(request.Username);
            if (user != null)
            {
                return new RegisterResponse { FailureMessage = $"Username {request.Username} is taken." };
            }
            user = _repo.Create(new User
            {
                Username = request.Username,
                PasswordHashed = hashed,
            });
            _repo.Save();
            return new RegisterResponse();
        }

        public SetRoleLevelResponse SetRoleLevel(SetRoleLevelRequest request)
        {
            Ensure.NotNull(request);
            var user = _repo.Find(request.UserId);
            if (user is null) { return new SetRoleLevelResponse { FailureMessage = "User not found." }; };
            user.Level = request.Level;
            user.Role = request.Role;
            _repo.Update(user);
            _repo.Save();
            return new SetRoleLevelResponse();
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