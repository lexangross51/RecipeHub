namespace RecipeHub.WebServer.DtoModels;

public record CreateProductDto
{
    public string Name { get; set; } = default!;

    public IFormFile? Image { get; set; }
}