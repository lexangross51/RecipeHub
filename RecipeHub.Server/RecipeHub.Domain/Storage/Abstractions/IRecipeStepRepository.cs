using RecipeHub.Domain.Models;

namespace RecipeHub.Domain.Storage.Abstractions;

public interface IRecipeStepRepository : IRepository<string, RecipeStep>;