namespace RecipeHub.WebServer.DtoModels.Products;

public class UpdateProductDto
{
    public required string Id { get; set; }

    public required string NewName { get; set; }

    public IFormFile? NewImage { get; set; }
}