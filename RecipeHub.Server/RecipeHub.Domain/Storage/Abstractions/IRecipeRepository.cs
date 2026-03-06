using RecipeHub.Domain.Models;

namespace RecipeHub.Domain.Storage.Abstractions;

public interface IRecipeRepository : IRepository<string, Recipe>
{
    Task<Recipe?> GetWithRelatedDataAsync(string id, CancellationToken token = default);
}