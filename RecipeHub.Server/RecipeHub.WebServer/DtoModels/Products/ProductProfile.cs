using AutoMapper;
using RecipeHub.Application.Mapping.ProductMapping;
using RecipeHub.Application.Products.Commands.CreateProduct;
using RecipeHub.Application.Products.Commands.UpdateProduct;

namespace RecipeHub.WebServer.DtoModels.Products;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        // WebServer DTO -> Application DTO
        CreateMap<CreateProductDto, CreateProductCommand>();
        CreateMap<UpdateProductDto, UpdateProductCommand>();

        // Application DTO -> WebServer DTO
        CreateMap<ProductDto, GetProductDto>();
    }
}