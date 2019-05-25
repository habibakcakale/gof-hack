using FluentValidation;

namespace Hack.Service
{
    public sealed class SetJiraCredentialsValidator : AbstractValidator<SetJiraCredentialsRequest>
    {
        public SetJiraCredentialsValidator()
        {
            RuleFor(o => o.Credentials).NotNull();
            RuleFor(o => o.Credentials.Username).EmailAddress();
            RuleFor(o => o.Credentials.Token).NotEmpty();
        }
    }
}