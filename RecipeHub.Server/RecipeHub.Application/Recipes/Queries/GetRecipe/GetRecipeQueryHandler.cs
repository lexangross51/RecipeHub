using AutoMapper;
using FluentResults;
using FluentValidation;
using RecipeHub.Application.Common;
using RecipeHub.Application.Mapping.RecipeMapping;
using RecipeHub.Domain.Storage.Abstractions;

namespace RecipeHub.Application.Recipes.Queries.GetRecipe;

internal class GetRecipeQueryHandler(IRecipeRepository repos, IMapper mapper, IValidator<GetRecipeQuery> validator)
    : RequestHandler<GetRecipeQuery, RecipeDto>(validator)
{
    protected override async Task<Result<RecipeDto>> HandleAsync(GetRecipeQuery request, CancellationToken cancellationToken)
    {
        var recipe = await repos.GetAsync(request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (recipe == null)
        {
            string errorMessage = $"Не удалось найти рецепт с id = {request.Id}";
            return Result.Fail(errorMessage);
        }

        var recipeDto = mapper.Map<RecipeDto>(recipe);
        return Result.Ok(recipeDto);
    }
}