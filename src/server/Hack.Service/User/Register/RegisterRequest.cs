namespace Hack.Service
{
    public sealed class RegisterRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }

        //public RegisterRequest()
        //{
        //    var cred = HackConfig.Instance.Auth.Credentials;
        //    RuleFor(o => o.Username).MinimumLength(cred.UsernameMinLength).MaximumLength(cred.UsernameMaxLength);
        //    RuleFor(o => o.Email).EmailAddress();
        //    RuleFor(o => o.Password).Equal(o => o.PasswordConfirm).MinimumLength(cred.PasswordMinLength).MaximumLength(cred.PasswordMaxLength);
        //}
    }
}