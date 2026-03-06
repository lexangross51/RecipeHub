using RecipeHub.Application.Mapping.RecipeMapping.Create;
using RecipeHub.Application.Mapping.RecipeMapping.Update;

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

    public IEnumerable<UpdateRecipeStepDto> EditedSteps { get; set; } = default!;

    public IEnumerable<Create.CreateRecipeStepDto>? NewSteps { get; set; }
}