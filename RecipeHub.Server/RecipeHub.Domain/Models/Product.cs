using RecipeHub.Domain.Models.Abstractions;

namespace RecipeHub.Domain.Models;

public sealed class Product : IEntity<string>
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Name { get; set; }

    public string? ImageId { get; set; }

    public Image? Image { get; set; }

    public Product() => Name = "unknown";

    public Product(string name) => Name = name;
}