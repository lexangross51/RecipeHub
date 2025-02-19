using RecipeHub.Domain.Models;

namespace RecipeHub.Application.Mapping.RecipeMapping;

public class RecipeDto
{
    public required string Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public FilePath? ImagePath { get; set; }

    public TimeSpan? CookingTime { get; set; }

    public IEnumerable<IngredientDto> Ingredients { get; set; } = default!;

    public IEnumerable<RecipeStepDto> RecipeSteps { get; set; } = default!;
}