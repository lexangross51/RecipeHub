using RecipeHub.Domain.Models;

namespace RecipeHub.Application.Mapping.RecipeMapping.Update;

public class UpdateRecipeStepDto
{
    public required string Id { get; set; }

    public int NewNumber { get; set; }

    public string NewDescription { get; set; } = default!;

    public Image? NewStepImage { get; set; }
}