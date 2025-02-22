using RecipeHub.Domain.Models;

namespace RecipeHub.Application.Mapping.RecipeMapping.Update;

public class UpdateIngredientDto
{
    public required string Id { get; set; }

    public Measure NewMeasure { get; set; } = default!;
}