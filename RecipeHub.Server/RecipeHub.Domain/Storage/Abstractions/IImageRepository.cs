using RecipeHub.Domain.Models;

namespace RecipeHub.Domain.Storage.Abstractions;

public interface IImageRepository : IRepository<string, Image>
{
    Task DeleteByIdAsync(params string[] ids);
}