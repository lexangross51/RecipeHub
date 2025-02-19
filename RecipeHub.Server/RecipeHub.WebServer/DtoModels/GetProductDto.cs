namespace RecipeHub.WebServer.DtoModels;

public class GetProductDto
{
    public string Name { get; set; } = default!;

    public byte[]? Image { get; set; }
}