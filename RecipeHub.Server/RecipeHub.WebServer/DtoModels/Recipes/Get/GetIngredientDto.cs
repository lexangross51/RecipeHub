using RecipeHub.Domain.Models;

namespace RecipeHub.WebServer.DtoModels.Recipes.Get;

public class GetIngredientDto
{
    public string Id { get; set; } = default!;

    public string Name { get; set; } = default!;

    public Measure Measure { get; set; } = default!;
}