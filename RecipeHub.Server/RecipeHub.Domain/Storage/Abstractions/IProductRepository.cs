using RecipeHub.Domain.Models;

namespace RecipeHub.Domain.Storage.Abstractions;

public interface IProductRepository : IRepository<string, Product>
{
    Task<Product?> GetByNameAsync(string name, CancellationToken token = default);
}