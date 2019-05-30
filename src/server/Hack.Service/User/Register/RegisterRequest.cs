using Hack.Domain;

namespace Hack.Service
{
    public sealed class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public UserLevel Level { get; set; }
        public UserRole Role { get; set; }
    }
}