using RecipeHub.Domain.Models;

namespace RecipeHub.WebServer.DtoModels.Recipes.Update;

public class UpdateIngredientDto
{
    public required string Id { get; set; }

    public Measure NewMeasure { get; set; } = default!;
}