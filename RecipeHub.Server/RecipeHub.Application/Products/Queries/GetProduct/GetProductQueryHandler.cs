using AutoMapper;
using FluentResults;
using FluentValidation;
using RecipeHub.Application.Common;
using RecipeHub.Application.Mapping.ProductMapping;
using RecipeHub.Domain.Storage.Abstractions;

namespace RecipeHub.Application.Products.Queries.GetProduct;

internal class GetProductQueryHandler(IProductRepository repos, IValidator<GetProductQuery> validator, IMapper mapper) 
    : RequestHandler<GetProductQuery, ProductDto>(validator)
{
    protected override async Task<Result<ProductDto>> HandleAsync(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await repos.GetAsync(request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (product == null)
        {
            string errorMessage = $"Не удалось получить продукт с id = {request.Id}";
            return Result.Fail(errorMessage);
        }

        var vm = mapper.Map<ProductDto>(product);
        return Result.Ok(vm);
    }
}