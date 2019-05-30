using FluentValidation;

namespace Hack.Service
{
    public sealed class ProceedRequestValidator : AbstractValidator<ProceedRequest>
    {
        public ProceedRequestValidator()
        {
            RuleFor(o => o.ProjectId).NotEmpty();
            RuleForEach(o => o.Issues).SetValidator(new IssueValidator());
        }
    }
}