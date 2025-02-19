using FluentValidation;

namespace RecipeHub.Application.Recipes.Commands.DeleteRecipe;

internal class DeleteRecipeCommandValidator : AbstractValidator<DeleteRecipeCommand>
{
    public DeleteRecipeCommandValidator() => RuleFor(x => x.Id)
            .NotEmpty()
            .MinimumLength(1);
}