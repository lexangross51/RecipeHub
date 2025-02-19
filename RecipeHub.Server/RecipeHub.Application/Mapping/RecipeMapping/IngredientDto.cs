using RecipeHub.Domain.Models;

namespace RecipeHub.Application.Mapping.RecipeMapping;

public class IngredientDto
{
    public required string Id { get; set; }

    public required string Name { get; set; }

    public required Measure Measure { get; set; }
}