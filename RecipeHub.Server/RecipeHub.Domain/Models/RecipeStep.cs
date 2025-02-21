using RecipeHub.Domain.Models.Abstractions;

namespace RecipeHub.Domain.Models;

public sealed class RecipeStep : IEntity<string>
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Description { get; set; } = default!;

    public string? ImageId { get; set; }

    public Image? Image { get; set; }
}