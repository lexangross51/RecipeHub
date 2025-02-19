using RecipeHub.Domain.Models;
using RecipeHub.Domain.Storage.Abstractions;

namespace RecipeHub.Persistence.Storage;

internal class ImageRepository(RecipeHubContext context)
    : Repository<string, Image>(context), IImageRepository
{
}