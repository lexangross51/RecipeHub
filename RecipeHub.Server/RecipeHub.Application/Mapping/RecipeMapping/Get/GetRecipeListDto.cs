namespace RecipeHub.Application.Mapping.RecipeMapping.Get;

public class GetRecipeListDto
{
    public IList<GetRecipeListItemDto> Recipes { get; set; } = default!;
}