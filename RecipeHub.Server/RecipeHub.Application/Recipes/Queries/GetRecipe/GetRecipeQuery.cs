using FluentResults;
using MediatR;
using RecipeHub.Application.Mapping.RecipeMapping.Get;

namespace RecipeHub.Application.Recipes.Queries.GetRecipe;

public class GetRecipeQuery : IRequest<Result<GetRecipeDto>>
{
    public required string Id { get; set; }
}