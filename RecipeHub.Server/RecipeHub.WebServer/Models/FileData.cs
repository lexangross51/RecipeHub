namespace RecipeHub.WebServer.Models;

public class FileData
{
    public required string Name { get; set; }

    public Stream? Data { get; set; }
}