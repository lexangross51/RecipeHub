using RecipeHub.Domain.Models;

namespace RecipeHub.Domain.Storage.Abstractions;

public interface IRecipeRepository : IRepository<string, Recipe>
{
}