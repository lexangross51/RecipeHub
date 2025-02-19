using RecipeHub.Domain.Models;
using RecipeHub.WebServer.Models;

namespace RecipeHub.WebServer.Services.Abstractions;

public interface IFileManager
{
    Task<FilePath> SaveAsync(string fileName, Stream fileDate, CancellationToken token = default);

    Task<FileData?> GetAsync(FilePath filePath, CancellationToken token = default);
}