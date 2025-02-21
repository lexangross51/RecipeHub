namespace RecipeHub.Application.Mapping.RecipeMapping;

public class RecipeListDto
{
    public IList<RecipeListItemDto> Recipes { get; set; } = default!;
}