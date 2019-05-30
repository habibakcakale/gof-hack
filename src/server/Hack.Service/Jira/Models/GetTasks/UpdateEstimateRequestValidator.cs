using FluentValidation;

namespace Hack.Service
{
    public class UpdateEstimateRequestValidator : AbstractValidator<UpdateEstimateRequest>
    {
        public UpdateEstimateRequestValidator()
        {
            RuleFor(item => item.IssueIdOrKey).NotEmpty();
        }

    }
}