using System.Diagnostics.CodeAnalysis;

namespace RecipeHub.Domain.Models.Abstractions;

public interface IEntity<TId>
{
    [NotNull]
    TId Id { get; set; }
}