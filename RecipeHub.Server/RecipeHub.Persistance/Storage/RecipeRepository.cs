using Microsoft.EntityFrameworkCore;
using RecipeHub.Domain.Models;
using RecipeHub.Domain.Storage.Abstractions;

namespace RecipeHub.Persistence.Storage;

internal class RecipeRepository(RecipeHubContext context)
    : Repository<string, Recipe>(context), IRecipeRepository
{
    public async Task<Recipe?> GetWithRelatedDataAsync(string id, CancellationToken token = default)
    {
        var query = Entities.AsNoTracking()
            .Include(r => r.Ingredients).ThenInclude(i => i.Product)
            .Include(r => r.Steps).ThenInclude(s => s.Image)
            .Include(r => r.RecipeImage)
            .Where(r => r.Id == id);

        return await query.SingleOrDefaultAsync(token).ConfigureAwait(false);
    }

    public override async Task<IEnumerable<Recipe>> GetAsync(ISpecification<Recipe>? specification = null, CancellationToken token = default)
    {
        var query = Entities.AsNoTracking()
            .Include(r => r.Ingredients)
            .ThenInclude(i => i.Product)
            .AsQueryable();

        if (specification != null)
        {
            query = ApplySpecification(query, specification);
        }

        return await query.ToListAsync(token).ConfigureAwait(false);
    }
}