using Microsoft.EntityFrameworkCore;
using RecipeHub.Domain.Models;
using RecipeHub.Domain.Storage.Abstractions;

namespace RecipeHub.Persistence.Storage;

internal class ProductRepository(RecipeHubContext context)
    : Repository<string, Product>(context), IProductRepository
{
    public override async Task<Product?> GetAsync(string id, CancellationToken token = default)
        => await Entities.AsNoTracking()
        .Include(p => p.Image)
        .FirstOrDefaultAsync(e => e.Id.Equals(id), token)
        .ConfigureAwait(false);

    public async Task<Product?> GetByNameAsync(string name, CancellationToken token = default) 
        => await Entities.FirstOrDefaultAsync(e => e.Name == name, token)
        .ConfigureAwait(false);
}