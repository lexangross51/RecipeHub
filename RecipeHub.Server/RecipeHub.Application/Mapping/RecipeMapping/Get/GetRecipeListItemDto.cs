namespace RecipeHub.Application.Mapping.RecipeMapping.Get;

public class GetRecipeListItemDto
{
    public required string Id { get; set; }

    public required string Name { get; set; }

    public TimeSpan? CookingTime { get; set; }

    public string? RecipeImageId { get; set; }

    public IEnumerable<string>? Ingredients { get; set; }
}