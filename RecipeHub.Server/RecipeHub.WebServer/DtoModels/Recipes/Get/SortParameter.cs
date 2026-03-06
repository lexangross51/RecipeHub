namespace RecipeHub.WebServer.DtoModels.Recipes.Get;

public class SortParameter
{
    public string? Field { get; set; }

    public string Order { get; set; } = "asc";
}