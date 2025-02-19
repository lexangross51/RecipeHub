using FluentResults;
using MediatR;
using RecipeHub.Application.Mapping.ProductMapping;

namespace RecipeHub.Application.Products.Queries.GetProduct;

public class GetProductQuery : IRequest<Result<ProductDto>>
{
    public required string Id { get; set; }
}