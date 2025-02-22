namespace RecipeHub.Application.Mapping.RecipeMapping.Get;

public class GetRecipeDto
{
    public required string Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public string? RecipeImageId { get; set; }

    public TimeSpan? CookingTime { get; set; }

    public IList<GetIngredientDto> Ingredients { get; set; } = default!;

    public IList<GetRecipeStepDto> Steps { get; set; } = default!;
}