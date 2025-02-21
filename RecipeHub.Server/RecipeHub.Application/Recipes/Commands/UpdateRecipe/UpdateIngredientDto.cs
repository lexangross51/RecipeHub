using RecipeHub.Domain.Models;

namespace RecipeHub.Application.Recipes.Commands.UpdateRecipe;

public class UpdateIngredientDto
{
    public required string Id { get; set; }

    public Measure NewMeasure { get; set; } = default!;
}