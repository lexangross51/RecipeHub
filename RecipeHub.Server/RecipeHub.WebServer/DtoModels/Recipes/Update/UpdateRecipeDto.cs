using RecipeHub.Application.Recipes.Commands.CreateRecipe;
using RecipeHub.WebServer.DtoModels.Recipes.Create;

namespace RecipeHub.WebServer.DtoModels.Recipes.Update;

public class UpdateRecipeDto
{
    public required string Id { get; set; }

    public string NewName { get; set; } = default!;

    public string? NewDescription { get; set; }

    public TimeSpan? NewCookingTime { get; set; }

    public IFormFile? NewRecipeImage { get; set; }

    public IEnumerable<UpdateIngredientDto> EditedIngredients { get; set; } = default!;

    public IEnumerable<CreateIngredientDto>? NewIngredients { get; set; }

    public IEnumerable<UpdateStepDto> EditedSteps { get; set; } = default!;

    public IEnumerable<CreateStepDto>? NewSteps { get; set; }
}