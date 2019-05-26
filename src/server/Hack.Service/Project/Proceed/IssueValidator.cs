using FluentValidation;

namespace Hack.Service
{
    public sealed class IssueValidator : AbstractValidator<ProceedRequest.Issue>
    {
        public IssueValidator()
        {
            RuleFor(o => o.JiraId).NotEmpty();
            RuleFor(o => o.Estimate).NotEmpty();
            RuleFor(o => o.Title).NotEmpty();
            RuleFor(o => o.UserLevel).NotEmpty();
            RuleFor(o => o.UserRole).NotEmpty();
        }
    }
}