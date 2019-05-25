namespace Hack.Service
{
    public sealed class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}