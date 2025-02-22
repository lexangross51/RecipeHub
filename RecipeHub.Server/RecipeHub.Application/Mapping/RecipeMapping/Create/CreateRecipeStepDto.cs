using RecipeHub.Domain.Models;

namespace RecipeHub.Application.Mapping.RecipeMapping.Create;

public class CreateRecipeStepDto
{
    public int Number { get; set; }

    public string Description { get; set; } = default!;

    public Image? StepImage { get; set; }
}