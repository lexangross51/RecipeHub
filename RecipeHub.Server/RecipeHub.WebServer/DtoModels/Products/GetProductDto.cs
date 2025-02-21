namespace RecipeHub.WebServer.DtoModels.Products;

public class GetProductDto
{
    public string Id { get; set; } = default!;

    public string Name { get; set; } = default!;

    public string? ImageUrl { get; set; }
}