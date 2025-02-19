using RecipeHub.Domain.Models;

namespace RecipeHub.Application.Recipes.Commands.CreateRecipe;

public class CreateRecipeStepDto
{
    public string Description { get; set; } = default!;

    public Image? StepImage { get; set; }
}