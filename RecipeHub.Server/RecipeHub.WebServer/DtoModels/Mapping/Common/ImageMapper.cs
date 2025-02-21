using AutoMapper;
using RecipeHub.Domain.Models;

namespace RecipeHub.WebServer.DtoModels.Mapping.Common;

public class ImageMapper : Profile
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

    public ImageMapper() => CreateMap<IFormFile, Image>().ConvertUsing<IFormFileToImageConverter>();
}