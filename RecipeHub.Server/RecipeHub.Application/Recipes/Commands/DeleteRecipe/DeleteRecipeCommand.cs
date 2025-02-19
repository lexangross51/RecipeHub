using FluentResults;
using MediatR;

namespace RecipeHub.Application.Recipes.Commands.DeleteRecipe;

public class DeleteRecipeCommand : IRequest<Result>
{
    public required string Id { get; set; }
}