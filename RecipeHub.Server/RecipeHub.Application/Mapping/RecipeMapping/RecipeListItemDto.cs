namespace RecipeHub.Application.Mapping.RecipeMapping;

public class RecipeListItemDto
{
    public required string Id { get; set; }

    public required string Name { get; set; }

    public TimeSpan? CookingTime { get; set; }

    public string? ImageId { get; set; }

    public IEnumerable<string>? Ingredients { get; set; }
}