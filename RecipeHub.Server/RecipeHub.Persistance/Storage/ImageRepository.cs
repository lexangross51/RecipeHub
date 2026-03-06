using Microsoft.EntityFrameworkCore;
using RecipeHub.Domain.Models;
using RecipeHub.Domain.Storage.Abstractions;

namespace RecipeHub.Persistence.Storage;

internal class ImageRepository(RecipeHubContext context)
    : Repository<string, Image>(context), IImageRepository
{
    public async Task DeleteByIdAsync(params string[] ids)
    {
        if (ids is not { Length: > 0  })
        {
            return;
        }

        await Entities
            .Where(i => ids.Contains(i.Id))
            .ExecuteDeleteAsync()
            .ConfigureAwait(false);
    }
}