using RecipeHub.Domain.Models.Abstractions;

namespace RecipeHub.Domain.Models;

public class Image : IEntity<string>
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string? Name { get; set; }

    public Stream? Data { get; set; }

    public FilePath? Path { get; set; } = default!;
}