using RecipeHub.Domain.Models.Abstractions;

namespace RecipeHub.Domain.Storage.Abstractions;

public interface IRepository<TId, TEntity> where TEntity : IEntity<TId>
{
    Task<IEnumerable<TEntity>> GetAsync(ISpecification<TEntity>? specification = default, CancellationToken token = default);

    Task<TEntity?> GetAsync(TId id, CancellationToken token = default);

    Task UpdateAsync(TEntity entity, CancellationToken token = default);

    Task DeleteAsync(TId id, CancellationToken token = default);

    Task CreateAsync(TEntity entity, CancellationToken token = default);

    Task CommitAsync(CancellationToken token = default);
}