namespace RecipeHub.Application.Mapping.RecipeMapping;

public class RecipeListDto
{
    public IEnumerable<RecipeListItemDto> Recipes { get; set; } = default!;
}