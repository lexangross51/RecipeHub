using AutoMapper;
using FluentResults;
using FluentValidation;
using RecipeHub.Application.Common;
using RecipeHub.Domain.Models;
using RecipeHub.Domain.Services.Abstractions;
using RecipeHub.Domain.Storage.Abstractions;

namespace RecipeHub.Application.Recipes.Commands.UpdateRecipe;

internal class UpdateRecipeCommandHandler(IUnitOfWork uow, IValidator<UpdateRecipeCommand> validator,
    IFileManager fileManager, IMapper mapper) : RequestHandler<UpdateRecipeCommand>(validator)
{
    protected override async Task<Result> HandleAsync(UpdateRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipeRepos = uow.GetRepository<IRecipeRepository>();
        var imageRepos = uow.GetRepository<IImageRepository>();
        var recipeStepRepos = uow.GetRepository<IRecipeStepRepository>();
        var ingredientRepos = uow.GetRepository<IIngredientRepository>();

        var recipe = await recipeRepos!.GetWithRelatedDataAsync(request.Id, cancellationToken)
            .ConfigureAwait(false);

        var imagesToDelete = new List<Image>();
        var addedImages = new List<Image>();

        try
        {
            if (recipe == null)
            {
                string errorMessage = $"Не удалось найти рецепт с id = {request.Id}";
                return Result.Fail(errorMessage);
            }

            if (recipe.Name != request.NewName)
            {
                recipe.Name = request.NewName;
            }

            if (recipe.CookingTime != request.NewCookingTime)
            {
                recipe.CookingTime = request.NewCookingTime;
            }

            if (recipe.Description != request.NewDescription)
            {
                recipe.Description = request.NewDescription;
            }

            if (request.NewRecipeImage is { Data: not null })
            {
                await SaveImageAsync(request.NewRecipeImage, cancellationToken).ConfigureAwait(false);
                await imageRepos!.CreateAsync(request.NewRecipeImage, cancellationToken).ConfigureAwait(false);
                addedImages.Add(request.NewRecipeImage);

                MarkAsDeleted(recipe.RecipeImage, imagesToDelete);
                recipe.RecipeImageId = request.NewRecipeImage.Id;
                recipe.RecipeImage = request.NewRecipeImage;
            }

            // Обновляем существующие ингредиенты и добавляем новые
            var ingToDelete = new List<Ingredient>();

            foreach (var i in recipe.Ingredients)
            {
                var ingredient = request.EditedIngredients.FirstOrDefault(ei => ei.Id == i.Id);

                if (ingredient == null)
                {
                    ingToDelete.Add(i);
                    continue;
                }

                if (i.Measure != ingredient.NewMeasure)
                {
                    i.Measure = ingredient.NewMeasure;
                }
            }

            foreach (var i in ingToDelete)
            {
                recipe.Ingredients.Remove(i);
                await ingredientRepos!.DeleteAsync(i.Id, cancellationToken).ConfigureAwait(false);
            }

            if (request.NewIngredients != null)
            {
                foreach (var newIng in request.NewIngredients)
                {
                    var ingredient = new Ingredient
                    {
                        ProductId = newIng.ProductId,
                        Measure = newIng.Measure
                    };

                    await ingredientRepos!.CreateAsync(ingredient, cancellationToken).ConfigureAwait(false);
                    recipe.Ingredients.Add(ingredient);
                }
            }

            // Обновляем существующие шаги и добавляем новые
            var stepsToDelete = new List<RecipeStep>();

            foreach (var s in recipe.Steps)
            {
                var step = request.EditedSteps.FirstOrDefault(step => step.Id == s.Id);

                if (step == null)
                {
                    stepsToDelete.Add(s);
                    continue;
                }

                if (s.Description != step.NewDescription)
                {
                    s.Description = step.NewDescription;
                }

                if (step.NewStepImage is { Data: not null })
                {
                    await SaveImageAsync(step.NewStepImage, cancellationToken).ConfigureAwait(false);
                    await imageRepos!.CreateAsync(step.NewStepImage, cancellationToken).ConfigureAwait(false);
                    addedImages.Add(step.NewStepImage);

                    MarkAsDeleted(s.Image, imagesToDelete);
                    s.ImageId = step.NewStepImage.Id;
                    s.Image = step.NewStepImage;
                }
            }

            foreach (var s in stepsToDelete)
            {
                await recipeStepRepos!.DeleteAsync(s.Id, cancellationToken).ConfigureAwait(false);
                recipe.Steps.Remove(s);
            }

            if (request.NewSteps != null)
            {
                foreach (var newStep in request.NewSteps)
                {
                    var newRecipeStep = mapper.Map<RecipeStep>(newStep);

                    if (newStep.StepImage is { Data: not null })
                    {
                        await SaveImageAsync(newStep.StepImage, cancellationToken).ConfigureAwait(false);
                        await imageRepos!.CreateAsync(newStep.StepImage, cancellationToken).ConfigureAwait(false);
                        addedImages.Add(newStep.StepImage);

                        newRecipeStep.Image = newStep.StepImage;
                        newRecipeStep.ImageId = newStep.StepImage.Id;
                    }

                    await recipeStepRepos!.CreateAsync(newRecipeStep, cancellationToken).ConfigureAwait(false);
                    recipe.Steps.Add(newRecipeStep);
                }
            }

            foreach (var i in imagesToDelete)
            {
                if (i == null) continue;

                await imageRepos!.DeleteAsync(i.Id, cancellationToken).ConfigureAwait(false);
            }

            await recipeRepos.UpdateAsync(recipe, cancellationToken).ConfigureAwait(false);
            await uow.CommitAsync(cancellationToken).ConfigureAwait(false);
            await DeleteImagesFromServer(imagesToDelete, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception)
        {
            await DeleteImagesFromServer(addedImages, cancellationToken).ConfigureAwait(false);
            throw;
        }

        return Result.Ok();
    }

    private static void MarkAsDeleted(Image? image, List<Image> toDelete)
    {
        if (image == null)
        {
            return;
        }

        toDelete.Add(image);
    }

    private async Task DeleteImagesFromServer(List<Image> toDelete, CancellationToken token)
    {
        foreach (var i in toDelete)
        {
            if (i is not { Path: not null }) continue;

            await fileManager.DeleteAsync(i.Path, token).ConfigureAwait(false);
        }
    }

    private async Task SaveImageAsync(Image image, CancellationToken token)
    {
        string fileName = $"{image.Id}_{image.Name}";
        await fileManager.SaveAsync(fileName, image, token);
    }
}