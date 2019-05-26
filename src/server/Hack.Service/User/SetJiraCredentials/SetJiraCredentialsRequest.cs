using Hack.Domain;

namespace Hack.Service
{
    public sealed class SetJiraCredentialsRequest
    {
        public JiraCredentialsConfig Credentials { get; set; }

        //public SetJiraCredentialsRequest()
        //{
        //    RuleFor(o => o.UserId).NotEmpty();
        //    RuleFor(o => o.Credentials).NotNull();
        //    RuleFor(o => o.Credentials.Username).NotEmpty();
        //    RuleFor(o => o.Credentials.Token).NotEmpty();
        //}
    }
}