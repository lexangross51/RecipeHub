namespace RecipeHub.WebServer.DtoModels.Recipes.Update;

public class UpdateRecipeStepDto
{
    public required string Id { get; set; }

    public int NewNumber { get; set; }

    public IFormFile? NewStepImage { get; set; }

    public string? NewDescription { get; set; }
}