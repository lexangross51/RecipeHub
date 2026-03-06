using RecipeHub.Application.Mapping.RecipeMapping.Get;

namespace RecipeHub.WebServer.DtoModels.Recipes.Get;

public class GetRecipeDto
{
    public string Id { get; set; } = default!;

    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public TimeSpan? CookingTime { get; set; }

    public IList<GetIngredientDto> Ingredients { get; set; } = default!;

    public IList<GetRecipeStepDto> Steps { get; set; } = default!;
}