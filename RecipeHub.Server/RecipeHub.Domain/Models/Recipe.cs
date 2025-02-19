using RecipeHub.Domain.Models.Abstractions;

namespace RecipeHub.Domain.Models;

public sealed class Recipe : IEntity<string>
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public required string Name { get; set; }

    public TimeSpan? CookingTime { get; set; }

    public string? Description { get; set; }

    public Image? RecipeImage { get; set; }

    public ICollection<Ingredient> Ingredients { get; set; } = default!;

    public ICollection<RecipeStep> Steps { get; set; } = default!;
}