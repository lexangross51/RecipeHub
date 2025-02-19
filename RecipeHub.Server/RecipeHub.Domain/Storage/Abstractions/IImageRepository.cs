using RecipeHub.Domain.Models;

namespace RecipeHub.Domain.Storage.Abstractions;

public interface IImageRepository : IRepository<string, Image>
{
}