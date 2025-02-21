namespace RecipeHub.Application.Mapping.RecipeMapping;

public class RecipeStepDto
{
    public required string Id { get; set; }

    public string Description { get; set; } = default!;

    public string? ImageId { get; set; }
}