using RecipeHub.Domain.Models.Abstractions;

namespace RecipeHub.Domain.Models;

public class Ingredient : IEntity<string>
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string ProductId { get; set; } = default!;

    public Product Product { get; set; } = default!;

    public Measure Measure { get; set; } = default!;
}