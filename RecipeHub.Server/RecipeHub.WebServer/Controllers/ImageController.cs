using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Caching.Memory;
using RecipeHub.Application.Images.Queries;
using RecipeHub.Domain.Models;

namespace RecipeHub.WebServer.Controllers;

[Route("api/v1/images")]
[ApiController]
public class ImageController(IMediator mediator, IMemoryCache cache, ILogger<ImageController> logger) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        try
        {
            FilePath? imagePath = default;

            if (cache.TryGetValue<FilePath>(id, out var pathFromCache))
            {
                imagePath = pathFromCache;
            }
            else
            {
                var result = await mediator.Send(new GetImageQuery { Id = id });

                if (result.IsFailed)
                {
                    return BadRequest(result.Errors.First());
                }

                logger.LogInformation($"Изображение с Id={id} успешно получено");
                cache.Set(id, result.Value.ImagePath, DateTimeOffset.Now.Add(MemoryCachingSettings.AbsoluteExpiration));
                imagePath = result.Value.ImagePath;
            }

            string path = imagePath?.PathLocation ?? string.Empty;
            string fileName = Path.GetFileName(path);
            string mime = new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var type)
                ? type
                : "image/png";

            return string.IsNullOrEmpty(path) || !System.IO.File.Exists(path)
                ? NoContent()
                : PhysicalFile(path, mime);
        }
        catch (Exception ex)
        {
            logger.LogError($"При получении изображения с Id={id} произошла ошибка: {ex.Message}");
            return NoContent();
        }
    }
}