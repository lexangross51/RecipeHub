using RecipeHub.Domain.Models;
using RecipeHub.WebServer.Models;
using RecipeHub.WebServer.Services.Abstractions;

namespace RecipeHub.WebServer.Services;

internal class LocalFileManager : IFileManager
{
    private const string ServerType = "localhost";
    private readonly string PathToSave = $@"{Directory.GetCurrentDirectory()}\wwwroot\images\";

    public LocalFileManager()
    {
        if (!Directory.Exists(PathToSave))
        {
            Directory.CreateDirectory(PathToSave);
        }
    }

    public Task<FileData?> GetAsync(FilePath filePath, CancellationToken token = default)
    {
        if (filePath.Server != ServerType)
        {
            throw new InvalidOperationException("Невозможно получить файл, т.к. он находится на другом сервере");
        }

        if (!File.Exists(filePath.PathLocation))
        {
            return Task.FromResult(default(FileData));
        }

        string fileName = Path.GetFileName(filePath.PathLocation);
        var fs = new FileStream(filePath.PathLocation, FileMode.Open, FileAccess.Read);

        return Task.FromResult<FileData?>(new FileData
        {
            Name = fileName,
            Data = fs
        });
    }

    public async Task<FilePath> SaveAsync(string fileName, Stream fileData, CancellationToken token = default)
    {
        string fileFullPath = Path.Combine(PathToSave, fileName);

        using var fileStream = new FileStream(fileFullPath, FileMode.Create);
        await fileData.CopyToAsync(fileStream, token).ConfigureAwait(false);

        return new FilePath
        {
            Server = ServerType,
            PathLocation = fileFullPath
        };
    }
}