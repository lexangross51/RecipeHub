using AutoMapper;
using FluentResults;
using FluentValidation;
using RecipeHub.Application.Common;
using RecipeHub.Application.Mapping.RecipeMapping.Get;
using RecipeHub.Domain.Storage.Abstractions;

namespace RecipeHub.Application.Recipes.Queries.GetRecipe;

internal class GetRecipeQueryHandler(IRecipeRepository repos, IMapper mapper, IValidator<GetRecipeQuery> validator)
    : RequestHandler<GetRecipeQuery, GetRecipeDto>(validator)
{
    protected override async Task<Result<GetRecipeDto>> HandleAsync(GetRecipeQuery request, CancellationToken cancellationToken)
    {
        var recipe = await repos.GetWithRelatedDataAsync(request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (recipe == null)
        {
            string errorMessage = $"Не удалось найти рецепт с id = {request.Id}";
            return Result.Fail(errorMessage);
        }

        var recipeDto = mapper.Map<GetRecipeDto>(recipe);
        return Result.Ok(recipeDto);
    }
}