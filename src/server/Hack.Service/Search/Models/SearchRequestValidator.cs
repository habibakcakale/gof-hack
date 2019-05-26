using FluentValidation;

namespace Hack.Service.Search
{
    public class SearchRequestValidator : AbstractValidator<SearchRequest>
    {
        public SearchRequestValidator()
        {
            RuleFor(prop => prop.Issue).NotNull();
            RuleFor(prop => prop.Issue.Fields).NotNull();

            RuleFor(prop => prop.Issue.Fields.UserLevel).NotNull();
            RuleFor(prop => prop.Issue.Fields.UserLevel.Value).NotEmpty();

            RuleFor(prop => prop.Issue.Fields.UserRole).NotNull();
            RuleFor(prop => prop.Issue.Fields.UserRole.Value).NotEmpty();

            RuleFor(prop => prop.Issue.Fields.Platform).NotNull();
            RuleFor(prop => prop.Issue.Fields.Platform.Value).NotEmpty();
        }
    }
}