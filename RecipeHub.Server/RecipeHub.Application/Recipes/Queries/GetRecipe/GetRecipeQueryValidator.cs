using FluentValidation;

namespace RecipeHub.Application.Recipes.Queries.GetRecipe;

internal class GetRecipeQueryValidator : AbstractValidator<GetRecipeQuery>
{
    public GetRecipeQueryValidator() => RuleFor(x => x.Id)
        .NotEmpty()
        .MinimumLength(1);
}