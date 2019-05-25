using Hack.Domain;

namespace Hack.Service
{
    public sealed class RegisterResponse
    {
        public User User { get; set; }
        public string FailureMessage { get; set; }
    }
}