namespace RecipeHub.WebServer.DtoModels.Recipes.Get;

public class GetRecipeStepDto
{
    public string Id { get; set; } = default!;

    public string Description { get; set; } = default!;

    public string? ImageUrl { get; set; }
}