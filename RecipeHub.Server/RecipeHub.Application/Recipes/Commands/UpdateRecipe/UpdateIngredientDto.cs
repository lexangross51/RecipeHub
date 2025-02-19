using RecipeHub.Domain.Models;

namespace RecipeHub.Application.Recipes.Commands.UpdateRecipe;

public class UpdateIngredientDto
{
    public required string Id { get; set; }

    public string ProductId { get; set; } = default!;

    public Measure Measure { get; set; } = default!;
}