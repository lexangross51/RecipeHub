using RecipeHub.Domain.Models;

namespace RecipeHub.Domain.Storage.Abstractions;

public interface IIngredientRepository : IRepository<string, Ingredient>;