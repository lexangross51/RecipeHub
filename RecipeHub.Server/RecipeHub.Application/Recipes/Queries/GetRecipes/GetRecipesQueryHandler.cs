using AutoMapper;
using FluentResults;
using RecipeHub.Application.Common;
using RecipeHub.Application.Mapping.RecipeMapping;
using RecipeHub.Domain.Storage.Abstractions;

namespace RecipeHub.Application.Recipes.Queries.GetRecipes;

internal class GetRecipesQueryHandler(IRecipeRepository repos, IMapper mapper)
    : RequestHandler<GetRecipesQuery, RecipeListDto>
{
    protected override async Task<Result<RecipeListDto>> HandleAsync(GetRecipesQuery request, CancellationToken cancellationToken)
    {
        var recipes = await repos.GetAsync(request.Specification, cancellationToken)
            .ConfigureAwait(false);

        var recipeDtos = new List<RecipeListItemDto>();

        foreach (var recipe in recipes)
        {
            var vm = mapper.Map<RecipeListItemDto>(recipe);
            recipeDtos.Add(vm);
        }

        return Result.Ok(new RecipeListDto { Recipes = recipeDtos });
    }
}