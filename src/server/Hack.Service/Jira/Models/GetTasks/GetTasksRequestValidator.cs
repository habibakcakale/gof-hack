using FluentValidation;

namespace Hack.Service
{
    public sealed class GetTasksRequestValidator : AbstractValidator<GetTasksRequest>
    {
        public GetTasksRequestValidator()
        {
            RuleFor(o => o.ProjectId).NotEmpty();
        }
    }
}