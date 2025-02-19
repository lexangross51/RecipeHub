namespace RecipeHub.Application.Mapping.ProductMapping;

public class ProductDto
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public string? ImageId { get; set; }
}