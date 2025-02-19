using FluentValidation;

namespace RecipeHub.Application.Images.Queries;

internal class GetImageQueryValidator : AbstractValidator<GetImageQuery>
{
    public GetImageQueryValidator() => RuleFor(x => x.Id)
        .NotEmpty();
}