namespace RecipeHub.WebServer.DtoModels.Recipes.Create;

public class CreateRecipeStepDto
{
    public int Number { get; set; }

    public string Description { get; set; } = default!;

    public IFormFile? StepImage { get; set; }
}