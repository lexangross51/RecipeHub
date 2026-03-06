namespace RecipeHub.Application.Mapping.RecipeMapping.Get;

public class GetRecipeStepDto
{
    public required string Id { get; set; }

    public int Number { get; set; }

    public string Description { get; set; } = default!;

    public string? ImageId { get; set; }
}