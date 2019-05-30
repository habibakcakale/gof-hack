using FluentValidation;

namespace Hack.Service
{
    public sealed class GetUserRequestValidator : AbstractValidator<GetUserRequest>
    {
        public GetUserRequestValidator()
        {
            RuleFor(o => o)
                .Must(o => (o.Id != null || !string.IsNullOrEmpty(o.Username)) && !(o.Id != null && !string.IsNullOrEmpty(o.Username)))
                .WithMessage("Specify either id or userName")
                .WithName("id-username");
        }
    }
}