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

    public IList<Expression<Func<Recipe, object?>>>? OrderBy { get; private set; }

    public IList<Expression<Func<Recipe, object?>>>? OrderByDescending { get; private set; }

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

        if (sortBy is not { Length: > 0 })
        {
            return;
        }

        var recipeType = typeof(Recipe);
        var properties = recipeType.GetProperties();
        
        foreach (var sort in sortBy)
        {
            if (string.IsNullOrEmpty(sort.Field))
            {
                continue;
            }

            var property = properties.FirstOrDefault(p => p.Name.Equals(sort.Field, StringComparison.OrdinalIgnoreCase))
                ?? throw new Exception($"Для типа \"{recipeType.Name}\" не определено свойство \"{sort.Field}\"");

            var parameter = Expression.Parameter(recipeType, "r");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var convert = Expression.Convert(propertyAccess, typeof(object));
            var lambda = Expression.Lambda<Func<Recipe, object?>>(convert, parameter);

            bool isAscending = true;

            if (sort.Order.Equals("desc", StringComparison.OrdinalIgnoreCase))
            {
                isAscending = false;
            }

            if (isAscending)
            {
                OrderBy ??= [];
                OrderBy.Add(lambda);
            }
            else
            {
                OrderByDescending ??= [];
                OrderByDescending.Add(lambda);
            }
        }
    }
}