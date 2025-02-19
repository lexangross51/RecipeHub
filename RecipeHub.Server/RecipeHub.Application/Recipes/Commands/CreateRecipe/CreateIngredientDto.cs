using RecipeHub.Domain.Models;

namespace RecipeHub.Application.Recipes.Commands.CreateRecipe;

public class CreateIngredientDto
{
    public string ProductId { get; set; } = default!;

    public Measure Measure { get; set; } = default!;
}