using FluentResults;
using FluentValidation;
using RecipeHub.Application.Common;
using RecipeHub.Domain.Models;
using RecipeHub.Domain.Services.Abstractions;
using RecipeHub.Domain.Storage.Abstractions;

namespace RecipeHub.Application.Recipes.Commands.DeleteRecipe;

internal class DeleteRecipeCommandHandler(IUnitOfWork uow, IValidator<DeleteRecipeCommand> validator, IFileManager fileManager)
    : RequestHandler<DeleteRecipeCommand>(validator)
{
    protected override async Task<Result> HandleAsync(DeleteRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipeRepos = uow.GetRepository<IRecipeRepository>();
        var imageRepos = uow.GetRepository<IImageRepository>();

        var recipe = await recipeRepos!.GetWithRelatedDataAsync(request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (recipe == null)
        {
            return Result.Ok();
        }

        var images = new List<Image>();

        if (recipe.RecipeImage != null)
        {
            images.Add(recipe.RecipeImage);
        }

        foreach (var step in recipe.Steps)
        {
            if (step.Image != null)
            {
                images.Add(step.Image);
            }
        }

        recipe = null;

        await recipeRepos!.DeleteAsync(request.Id, cancellationToken).ConfigureAwait(false);
        await recipeRepos.CommitAsync(cancellationToken).ConfigureAwait(false);
        await imageRepos!.DeleteByIdAsync(images.Select(i => i.Id).ToArray()).ConfigureAwait(false);
        await imageRepos.CommitAsync(cancellationToken).ConfigureAwait(false);

        foreach (var image in images)
        {
            if (image.Path != null)
            {
                await fileManager.DeleteAsync(image.Path, cancellationToken).ConfigureAwait(false);
            }
        }

        return Result.Ok();
    }
}