using FluentValidation;
using Hack.Domain;

namespace Hack.Service
{
    public sealed class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            var cred = HackConfig.Instance.Auth.Credentials;
            RuleFor(o => o.Username).EmailAddress();
            RuleFor(o => o.Password).MinimumLength(cred.PasswordMinLength).MaximumLength(cred.PasswordMaxLength);
        }
    }
}