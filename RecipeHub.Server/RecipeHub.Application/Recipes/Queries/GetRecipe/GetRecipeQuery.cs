using FluentResults;
using MediatR;
using RecipeHub.Application.Mapping.RecipeMapping;

namespace RecipeHub.Application.Recipes.Queries.GetRecipe;

public class GetRecipeQuery : IRequest<Result<RecipeDto>>
{
    public required string Id { get; set; }
}