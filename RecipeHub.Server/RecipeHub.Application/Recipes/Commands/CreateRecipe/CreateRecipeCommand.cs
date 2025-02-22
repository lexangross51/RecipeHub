using FluentResults;
using MediatR;
using RecipeHub.Application.Mapping.RecipeMapping.Create;
using RecipeHub.Domain.Models;

namespace RecipeHub.Application.Recipes.Commands.CreateRecipe;

public class CreateRecipeCommand : IRequest<Result<string>>
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public Image? RecipeImage { get; set; }

    public TimeSpan? CookingTime { get; set; }

    public IEnumerable<CreateIngredientDto> Ingredients { get; set; } = default!;

    public IEnumerable<CreateRecipeStepDto> Steps { get; set; } = default!;
}