using RecipeHub.Domain.Models;

namespace RecipeHub.Application.Recipes.Commands.UpdateRecipe;

public class UpdateRecipeStepDto
{
    public required string Id { get; set; }

    public string NewDescription { get; set; } = default!;

    public Image? NewStepImage { get; set; }
}