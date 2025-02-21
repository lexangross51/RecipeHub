using RecipeHub.Domain.Models;
using RecipeHub.Domain.Storage.Abstractions;

namespace RecipeHub.Persistence.Storage;

internal class IngredientRepository(RecipeHubContext context)
        : Repository<string, Ingredient>(context), IIngredientRepository
{
}