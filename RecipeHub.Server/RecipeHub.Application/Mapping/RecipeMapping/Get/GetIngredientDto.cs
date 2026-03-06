using RecipeHub.Domain.Models;

namespace RecipeHub.Application.Mapping.RecipeMapping.Get;

public class GetIngredientDto
{
    public required string Id { get; set; }

    public required string Name { get; set; }

    public required Measure Measure { get; set; }
}