using RecipeHub.Domain.Models;

namespace RecipeHub.Application.Mapping.ImageMapping;

public class ImageDto
{
    public FilePath ImagePath { get; set; } = default!;
}