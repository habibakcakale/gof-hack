namespace Hack.Domain
{
    public interface IHackConfig
    {
        AuthenticationConfig Auth { get; }
        JiraCredentialsConfig JiraCredentials { get; }
    }
}