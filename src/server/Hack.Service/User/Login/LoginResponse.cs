using Hack.Domain;

namespace Hack.Service
{
    public sealed class LoginResponse
    {
        public User User { get; set; }
        public string Token { get; set; }
        public string FailureMessage { get; set; }
    }
}