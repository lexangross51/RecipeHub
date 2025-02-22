using RecipeHub.Application.Mapping.RecipeMapping.Create;

namespace RecipeHub.WebServer.DtoModels.Recipes.Create;

public class CreateRecipeDto
{
    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public TimeSpan? CookingTime { get; set; }

    public IFormFile? RecipeImage { get; set; }

    public IEnumerable<CreateIngredientDto> Ingredients { get; set; } = default!;

    public IEnumerable<CreateRecipeStepDto> Steps { get; set; } = default!;
}