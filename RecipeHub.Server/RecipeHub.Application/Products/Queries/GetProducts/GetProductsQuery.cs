using FluentResults;
using MediatR;
using RecipeHub.Application.Mapping.ProductMapping;
using RecipeHub.Domain.Models;
using RecipeHub.Domain.Storage.Abstractions;

namespace RecipeHub.Application.Products.Queries.GetProducts;

public class GetProductsQuery : IRequest<Result<ProductListDto>>
{
    public ISpecification<Product>? Specification { get; set; }
}