using FluentResults;
using FluentValidation;
using RecipeHub.Application.Common;
using RecipeHub.Domain.Storage.Abstractions;

namespace RecipeHub.Application.Recipes.Commands.UpdateRecipe;

// TODO
internal class UpdateRecipeCommandHandler(IRecipeRepository repos, IValidator<UpdateRecipeCommand> validator)
    : RequestHandler<UpdateRecipeCommand>(validator)
{
    protected override async Task<Result> HandleAsync(UpdateRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = await repos.GetAsync(request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (recipe == null)
        {
            string errorMessage = $"Не удалось найти рецепт с id = {request.Id}";
            return Result.Fail(errorMessage);
        }

        //recipe.Name = request.NewName;
        //recipe.Description = request.NewDescription;
        //recipe.RecipeImage = request.NewRecipeImage;
        //recipe.CookingTime = request.NewCookingTime;
        //recipe.Ingredients = request.NewIngredients;
        //recipe.Steps = request.NewSteps;

        await repos.UpdateAsync(recipe, cancellationToken).ConfigureAwait(false);
        await repos.CommitAsync(cancellationToken).ConfigureAwait(false);

        return Result.Ok();
    }
}