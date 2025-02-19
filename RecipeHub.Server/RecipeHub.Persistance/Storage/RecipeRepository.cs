using RecipeHub.Domain.Models;
using RecipeHub.Domain.Storage.Abstractions;

namespace RecipeHub.Persistence.Storage;

internal class RecipeRepository(RecipeHubContext context) 
    : Repository<string, Recipe>(context), IRecipeRepository
{
}