using RecipeHub.Domain.Models;

namespace RecipeHub.Domain.Services.Abstractions;

public interface IFileManager : IService
{
    Task<FilePath> SaveAsync(string fileName, Image image, CancellationToken token = default);

    Task<Image?> GetAsync(FilePath filePath, CancellationToken token = default);

    Task DeleteAsync(FilePath filePath, CancellationToken token = default);
}