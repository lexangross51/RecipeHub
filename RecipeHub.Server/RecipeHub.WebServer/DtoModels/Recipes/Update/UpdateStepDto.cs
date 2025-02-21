namespace RecipeHub.WebServer.DtoModels.Recipes.Update;

public class UpdateStepDto
{
    public required string Id { get; set; }

    public IFormFile? NewStepImage { get; set; }

    public string? NewDescription { get; set; }
}