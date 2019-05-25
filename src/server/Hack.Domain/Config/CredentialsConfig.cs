namespace Hack.Domain
{
    public sealed class CredentialsConfig
    {
        public int UsernameMinLength { get; set; }
        public int UsernameMaxLength { get; set; }
        public int PasswordMinLength { get; set; }
        public int PasswordMaxLength { get; set; }
    }
}