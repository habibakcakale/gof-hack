namespace Hack.Service
{
    public sealed class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }

        //public LoginRequest()
        //{
        //    var cred = HackConfig.Instance.Auth.Credentials;
        //    RuleFor(o => o.Username).MinimumLength(cred.UsernameMinLength).MaximumLength(cred.UsernameMaxLength);
        //    RuleFor(o => o.Password).MinimumLength(cred.PasswordMinLength).MaximumLength(cred.PasswordMaxLength);
        //}
    }
}