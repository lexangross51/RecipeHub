namespace RecipeHub.Domain.Storage.Abstractions;

public interface IUnitOfWork
{
    TRepository? GetRepository<TRepository>() where TRepository: notnull;

    Task CommitAsync(CancellationToken token = default);
}