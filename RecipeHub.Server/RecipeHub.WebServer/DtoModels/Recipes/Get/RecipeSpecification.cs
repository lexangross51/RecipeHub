using LinqKit;
using RecipeHub.Domain.Models;
using RecipeHub.Domain.Storage.Abstractions;
using System.Linq.Expressions;

namespace RecipeHub.WebServer.DtoModels.Recipes.Get;

public class RecipeSpecification : ISpecification<Recipe>
{
    public int? Take { get; private set; }

    public int? Skip { get; private set; }

    public Expression<Func<Recipe, bool>> Criteria { get; private set; } = default!;

    public Expression<Func<Recipe, object?>>? OrderBy { get; private set; }

    public Expression<Func<Recipe, object?>>? OrderByDescending { get; private set; }

    public RecipeSpecification(int? take = null, int? skip = null, SortParameter[]? sortBy = null,
        string? name = null, string[]? ingredients = null)
    {
        Take = take;
        Skip = skip;

        var criteria = PredicateBuilder.New<Recipe>(true);

        if (!string.IsNullOrEmpty(name))
        {
            criteria.And(r => r.Name.Contains(name));
        }

        if (ingredients != null)
        {
            criteria.And(r => ingredients.All(ingName => r.Ingredients.Any(i => i.Product.Name.Contains(ingName))));
        }

        Criteria = criteria;
    }
}