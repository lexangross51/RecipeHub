using RecipeHub.Domain.Models;
using RecipeHub.Domain.Services.Abstractions;

namespace RecipeHub.Persistence.Services;

internal class LocalFileManager : IFileManager
{
    private const string ServerType = "localhost";
    private readonly string PathToSave = $@"{Directory.GetCurrentDirectory()}\images\";

    public LocalFileManager()
    {
        if (!Directory.Exists(PathToSave))
        {
            Directory.CreateDirectory(PathToSave);
        }
    }

    public Task<Image?> GetAsync(FilePath filePath, CancellationToken token = default)
    {
        if (filePath.Server != ServerType)
        {
            throw new InvalidOperationException("Невозможно получить файл, т.к. он находится на другом сервере");
        }

        if (!File.Exists(filePath.PathLocation))
        {
            return Task.FromResult(default(Image));
        }

        var fs = new FileStream(filePath.PathLocation, FileMode.Open, FileAccess.Read);
        string fileName = Path.GetFileName(filePath.PathLocation);

        return Task.FromResult<Image?>(new Image
        {
            Name = fileName,
            Path = filePath,
            Data = fs
        });
    }

    public async Task<FilePath> SaveAsync(string fileName, Image image, CancellationToken token = default)
    {
        if (image.Data == null)
        {
            throw new InvalidOperationException("Файл не содержит данных");
        }

        string fileFullPath = Path.Combine(PathToSave, fileName);

        using var fileStream = new FileStream(fileFullPath, FileMode.Create);
        await image.Data.CopyToAsync(fileStream, token).ConfigureAwait(false);

        image.Path = new FilePath
        {
            Server = ServerType,
            PathLocation = fileFullPath
        };

        return image.Path;
    }

    public Task DeleteAsync(FilePath filePath, CancellationToken token = default)
    {
        if (filePath.Server != ServerType)
        {
            throw new InvalidOperationException("Невозможно удалить файл, т.к. он находится на другом сервере");
        }

        if (!File.Exists(filePath.PathLocation))
        {
            return Task.CompletedTask;
        }

        File.Delete(filePath.PathLocation);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
    }
}