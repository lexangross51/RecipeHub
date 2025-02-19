using FluentResults;
using FluentValidation;
using RecipeHub.Application.Common;
using RecipeHub.Domain.Models;
using RecipeHub.Domain.Services.Abstractions;
using RecipeHub.Domain.Storage.Abstractions;

namespace RecipeHub.Application.Recipes.Commands.CreateRecipe;

internal class CreateRecipeCommandHandler(IRecipeRepository repos, IValidator<CreateRecipeCommand> validator,
    IFileManager fileManager) : RequestHandler<CreateRecipeCommand, string>(validator)
{
    protected override async Task<Result<string>> HandleAsync(CreateRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = new Recipe
        {
            Name = request.Name,
            CookingTime = request.CookingTime,
            Description = request.Description,
            Ingredients = request.Ingredients.Select(i => new Ingredient
            {
                Product = new Product { Id = i.ProductId },
                Measure = i.Measure
            }).ToArray(),
            Steps = request.Steps.Select(s =>
            {
                var recipeStep = new RecipeStep { Description = s.Description };

                if (s.StepImage is { Data: not null })
                {
                    SaveImageAsync(s.StepImage, cancellationToken).Wait();
                    recipeStep.Image = s.StepImage;
                }

                return recipeStep;
            }).ToArray()
        };

        if (request.RecipeImage is { Data: not null })
        {
            await SaveImageAsync(request.RecipeImage, cancellationToken).ConfigureAwait(false);
            recipe.RecipeImage = request.RecipeImage;
        }

        await repos.CreateAsync(recipe, cancellationToken).ConfigureAwait(false);
        await repos.CommitAsync(cancellationToken).ConfigureAwait(false);

        return Result.Ok(recipe.Id);
    }

    private async Task SaveImageAsync(Image image, CancellationToken token)
    {
        string fileName = $"{image.Id}_{image.Name}";
        await fileManager.SaveAsync(fileName, image, token);
    }
}