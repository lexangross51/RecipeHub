namespace RecipeHub.WebServer.DtoModels.Recipes.Create;

public class RecipeStepDto
{
    public string Description { get; set; } = default!;

    public IFormFile? StepImage { get; set; }
}