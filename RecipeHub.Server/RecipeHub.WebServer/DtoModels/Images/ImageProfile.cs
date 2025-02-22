using AutoMapper;
using RecipeHub.Domain.Models;

namespace RecipeHub.WebServer.DtoModels.Images;

public class ImageProfile : Profile
{
    private class IFormFileToImageConverter : ITypeConverter<IFormFile, Image>
    {
        public Image Convert(IFormFile source, Image destination, ResolutionContext context)
        {
            if (source == null)
            {
                return new Image();
            }

            var memoryStream = new MemoryStream();
            source.CopyTo(memoryStream);
            memoryStream.Position = 0;

            return new Image
            {
                Name = source.FileName,
                Data = memoryStream
            };
        }
    }

    public ImageProfile() => CreateMap<IFormFile, Image>().ConvertUsing<IFormFileToImageConverter>();
}