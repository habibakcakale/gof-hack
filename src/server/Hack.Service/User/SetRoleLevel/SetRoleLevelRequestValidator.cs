using FluentValidation;

namespace Hack.Service
{
    public sealed class SetRoleLevelRequestValidator : AbstractValidator<SetRoleLevelRequest>
    {
        public SetRoleLevelRequestValidator()
        {
            RuleFor(o => o.Level).NotEmpty();
            RuleFor(o => o.Role).NotEmpty();
        }
    }
}