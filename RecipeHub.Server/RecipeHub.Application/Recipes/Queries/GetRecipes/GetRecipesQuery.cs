using FluentResults;
using MediatR;
using RecipeHub.Application.Mapping.RecipeMapping;
using RecipeHub.Domain.Models;
using RecipeHub.Domain.Storage.Abstractions;

namespace RecipeHub.Application.Recipes.Queries.GetRecipes;

public class GetRecipesQuery : IRequest<Result<RecipeListDto>>
{
    public ISpecification<Recipe>? Specification { get; set; }
}