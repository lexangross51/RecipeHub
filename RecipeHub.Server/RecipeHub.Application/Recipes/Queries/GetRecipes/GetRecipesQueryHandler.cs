using AutoMapper;
using FluentResults;
using RecipeHub.Application.Common;
using RecipeHub.Application.Mapping.RecipeMapping.Get;
using RecipeHub.Domain.Storage.Abstractions;

namespace RecipeHub.Application.Recipes.Queries.GetRecipes;

internal class GetRecipesQueryHandler(IRecipeRepository repos, IMapper mapper)
    : RequestHandler<GetRecipesQuery, GetRecipeListDto>
{
    protected override async Task<Result<GetRecipeListDto>> HandleAsync(GetRecipesQuery request, CancellationToken cancellationToken)
    {
        var recipes = await repos.GetAsync(request.Specification, cancellationToken)
            .ConfigureAwait(false);

        var recipeDtos = new List<GetRecipeListItemDto>();

        foreach (var recipe in recipes)
        {
            var vm = mapper.Map<GetRecipeListItemDto>(recipe);
            recipeDtos.Add(vm);
        }

        return Result.Ok(new GetRecipeListDto { Recipes = recipeDtos });
    }
}