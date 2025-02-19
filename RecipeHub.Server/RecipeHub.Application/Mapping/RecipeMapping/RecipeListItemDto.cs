using RecipeHub.Domain.Models;

namespace RecipeHub.Application.Mapping.RecipeMapping;

public class RecipeListItemDto
{
    public required string Name { get; set; }

    public TimeSpan? CookingTime { get; set; }

    public Image? Image { get; set; }

    public IEnumerable<string>? Ingredients { get; set; }
}