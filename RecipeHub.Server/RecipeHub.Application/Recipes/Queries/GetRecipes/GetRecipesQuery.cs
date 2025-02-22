using FluentResults;
using MediatR;
using RecipeHub.Application.Mapping.RecipeMapping.Get;
using RecipeHub.Domain.Models;
using RecipeHub.Domain.Storage.Abstractions;

namespace RecipeHub.Application.Recipes.Queries.GetRecipes;

public class GetRecipesQuery : IRequest<Result<GetRecipeListDto>>
{
    public ISpecification<Recipe>? Specification { get; set; }
}