namespace RecipeHub.WebServer.DtoModels.Recipes.Get;

public class GetRecipeListDto
{
    public string Id { get; set; } = default!;

    public string Name { get; set; } = default!;

    public TimeSpan? CookingTime { get; set; }

    public string? ImageUrl { get; set; }

    public IEnumerable<string>? Ingredients { get; set; }
}