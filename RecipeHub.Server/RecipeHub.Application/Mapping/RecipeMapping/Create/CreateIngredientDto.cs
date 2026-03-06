using RecipeHub.Domain.Models;

namespace RecipeHub.Application.Mapping.RecipeMapping.Create;

public class CreateIngredientDto
{
    public string ProductId { get; set; } = default!;

    public Measure Measure { get; set; } = default!;
}