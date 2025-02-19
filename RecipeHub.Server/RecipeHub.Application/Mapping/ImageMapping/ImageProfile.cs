using AutoMapper;
using RecipeHub.Domain.Models;

namespace RecipeHub.Application.Mapping.ImageMapping;

internal class ImageProfile : Profile
{
    public ImageProfile()
    {
        CreateMap<Image, ImageDto>()
            .ForMember(i => i.ImagePath,
            opt => opt.MapFrom(src => src.Path));
    }
}