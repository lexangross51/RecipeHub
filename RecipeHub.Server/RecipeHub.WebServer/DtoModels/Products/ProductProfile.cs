using AutoMapper;
using RecipeHub.Application.Mapping.ProductMapping;
using RecipeHub.Application.Products.Commands.CreateProduct;
using RecipeHub.Application.Products.Commands.UpdateProduct;

namespace RecipeHub.WebServer.DtoModels.Products;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductDto, CreateProductCommand>()
            .ForMember(cmd => cmd.Name,
            opt => opt.MapFrom(src => src.Name))
            .ForMember(cmd => cmd.Image,
            opt => opt.MapFrom(src => src.Image));

        CreateMap<UpdateProductDto, UpdateProductCommand>()
            .ForMember(cmd => cmd.Id,
            opt => opt.MapFrom(src => src.Id))
            .ForMember(cmd => cmd.NewName,
            opt => opt.MapFrom(src => src.NewName))
            .ForMember(cmd => cmd.NewImage,
            opt => opt.MapFrom(src => src.NewImage));

        CreateMap<ProductDto, GetProductDto>()
            .ForMember(dto => dto.Id,
            opt => opt.MapFrom(src => src.Id))
            .ForMember(dto => dto.Name,
            opt => opt.MapFrom(src => src.Name));
    }
}