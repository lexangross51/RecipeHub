using FluentValidation;

namespace RecipeHub.Application.Recipes.Commands.UpdateRecipe;

internal class UpdateRecipeCommandValidator : AbstractValidator<UpdateRecipeCommand>
{
    public UpdateRecipeCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .MinimumLength(1);

        RuleFor(x => x.NewName)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(256);

        RuleFor(x => x.EditedIngredients)
            .NotNull()
            .Must(d => d.Any());

        RuleFor(x => x.EditedSteps)
            .NotNull()
            .Must(s => s.Any());
    }
}