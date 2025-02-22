using System.Linq.Expressions;

namespace RecipeHub.Domain.Storage.Abstractions;

public interface ISpecification<T>
{
    int? Take { get; }

    int? Skip { get; }

    Expression<Func<T, bool>> Criteria { get; }

    IList<Expression<Func<T, object?>>>? OrderBy { get; }

    IList<Expression<Func<T, object?>>>? OrderByDescending { get; }
}