using FluentValidation;
using Hack.Domain;

namespace Hack.Service
{
    public sealed class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            var cred = HackConfig.Instance.Auth.Credentials;
            RuleFor(o => o.Username).EmailAddress();
            RuleFor(o => o.Password).MinimumLength(cred.PasswordMinLength).MaximumLength(cred.PasswordMaxLength);
            When(o => o.Password.Length >= cred.PasswordMinLength && o.Password.Length <= cred.PasswordMaxLength, () =>
            {
                RuleFor(o => o.PasswordConfirm).Equal(o => o.Password).WithMessage("Passwords do not match");
            });
        }
    }
}