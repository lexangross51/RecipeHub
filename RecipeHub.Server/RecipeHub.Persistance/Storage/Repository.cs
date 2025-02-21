using Microsoft.EntityFrameworkCore;
using RecipeHub.Domain.Models.Abstractions;
using RecipeHub.Domain.Storage.Abstractions;
using RecipeHub.Domain.Storage.Exceptions;

namespace RecipeHub.Persistence.Storage;

internal abstract class Repository<TId, TEntity>(RecipeHubContext context) 
    : IRepository<TId, TEntity>
    where TEntity : class, IEntity<TId>
{
    protected readonly DbSet<TEntity> Entities = context.Set<TEntity>();

    public virtual async Task CreateAsync(TEntity entity, CancellationToken token = default) 
        => await Entities.AddAsync(entity, token).ConfigureAwait(false);

    public virtual async Task DeleteAsync(TId id, CancellationToken token = default)
    {
        var toDelete = await Entities
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id.Equals(id), token)
            .ConfigureAwait(false) 
            ?? throw new EntityNotFoundException($"Сущность типа {typeof(TEntity)} c ID = {id} не найдена");
        Entities.Remove(toDelete);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAsync(ISpecification<TEntity>? specification = null, CancellationToken token = default)
    {
        var query = Entities.AsNoTracking().AsQueryable();

        if (specification != null)
        {
            query = ApplySpecification(query, specification);
        }

        return await query.ToListAsync(token).ConfigureAwait(false);
    }

    protected virtual IQueryable<TEntity> ApplySpecification(IQueryable<TEntity> query, ISpecification<TEntity> specification)
    {
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }

        if (specification.OrderByDescending != null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        if (specification.Take.HasValue)
        {
            query = query.Take(specification.Take.Value);
        }

        if (specification.Skip.HasValue)
        {
            query = query.Skip(specification.Skip.Value);
        }

        return query;
    }

    public virtual async Task<TEntity?> GetAsync(TId id, CancellationToken token = default) 
        => await Entities.AsNoTracking()
        .FirstOrDefaultAsync(e => e.Id.Equals(id), token)
        .ConfigureAwait(false);

    public virtual Task UpdateAsync(TEntity entity, CancellationToken token = default)
    {
        Entities.Update(entity);

        var entry = Entities.Entry(entity);

        return Task.CompletedTask;
    }

    public virtual async Task CommitAsync(CancellationToken token = default)
        => await context.SaveChangesAsync(token).ConfigureAwait(false);
}