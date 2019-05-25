namespace Hack.Domain
{
    public sealed class AuthenticationConfig
    {
        public TokenConfig Token { get; set; }
        public CredentialsConfig Credentials { get; set; }
    }
}