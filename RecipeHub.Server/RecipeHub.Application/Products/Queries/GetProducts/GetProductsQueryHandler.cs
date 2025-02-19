using AutoMapper;
using FluentResults;
using RecipeHub.Application.Common;
using RecipeHub.Application.Mapping.ProductMapping;
using RecipeHub.Domain.Storage.Abstractions;

namespace RecipeHub.Application.Products.Queries.GetProducts;

internal class GetProductsQueryHandler(IProductRepository repos, IMapper mapper) : RequestHandler<GetProductsQuery, ProductListDto>
{
    protected override async Task<Result<ProductListDto>> HandleAsync(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await repos.GetAsync(request.Specification, cancellationToken)
            .ConfigureAwait(false);

        var productVms = new List<ProductDto>();

        foreach (var product in products)
        {
            var vm = mapper.Map<ProductDto>(product);
            productVms.Add(vm);
        }

        return Result.Ok(new ProductListDto { Products = productVms });
    }
}