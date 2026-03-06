using AutoMapper;
using RecipeHub.Domain.Models;

namespace RecipeHub.Application.Mapping.ProductMapping;

internal class ProductProfile : Profile
{
	public ProductProfile()
	{
		CreateMap<Product, ProductDto>()
            .ForMember(x => x.ImageId,
            opt => opt.MapFrom(src => src.Image != null ? src.Image.Id : default));
	}
}