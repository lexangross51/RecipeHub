using Microsoft.Extensions.DependencyInjection;
using RecipeHub.Domain.Storage.Abstractions;

namespace RecipeHub.Persistence.Storage;

internal class UnitOfWork(IServiceProvider provider, RecipeHubContext context) : IUnitOfWork
{
    public TRepository? GetRepository<TRepository>() where TRepository : notnull
        => provider.GetRequiredService<TRepository>();

    public async Task CommitAsync(CancellationToken token = default)
        => await context.SaveChangesAsync(token).ConfigureAwait(false);
}