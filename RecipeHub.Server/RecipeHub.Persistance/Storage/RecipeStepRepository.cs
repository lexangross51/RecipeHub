using RecipeHub.Domain.Models;
using RecipeHub.Domain.Storage.Abstractions;

namespace RecipeHub.Persistence.Storage;

internal class RecipeStepRepository(RecipeHubContext context) 
    : Repository<string, RecipeStep>(context), IRecipeStepRepository
{
}