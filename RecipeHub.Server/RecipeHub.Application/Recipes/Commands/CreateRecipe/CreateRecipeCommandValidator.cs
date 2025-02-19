using FluentValidation;

namespace RecipeHub.Application.Recipes.Commands.CreateRecipe;

internal class CreateRecipeCommandValidator : AbstractValidator<CreateRecipeCommand>
{
    public CreateRecipeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(256);

        RuleFor(x => x.Ingredients)
            .NotNull()
            .Must(d => d.Any());

        RuleFor(x => x.Steps)
            .NotNull()
            .Must(s => s.Any());
    }
}