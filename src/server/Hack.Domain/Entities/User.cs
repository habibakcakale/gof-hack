namespace Hack.Domain
{
    public sealed class User : EntityBase
    {
        public string Username { get; set; }
        public string PasswordHashed { get; set; }
        public JiraCredentials Credentials { get; set; }
    }
}