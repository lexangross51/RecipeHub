using FluentValidation;
using RecipeHub.Domain.Models;
using RecipeHub.Domain.Storage.Abstractions;
using RecipeHub.Application.Common;
using FluentResults;
using RecipeHub.Domain.Services.Abstractions;

namespace RecipeHub.Application.Products.Commands.CreateProduct;

internal class CreateProductCommandHandler(IProductRepository repos, IFileManager fileManager,
    IValidator<CreateProductCommand> validator) : RequestHandler<CreateProductCommand, string>(validator)
{
    protected override async Task<Result<string>> HandleAsync(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await repos.GetByNameAsync(request.Name, cancellationToken)
            .ConfigureAwait(false);

        if (product != null)
        {
            string errorMessage = $"Невозможно создать продукт \"{request.Name}\", т.к. он уже существует";
            return Result.Fail<string>(errorMessage);
        }

        product = new Product(request.Name);

        if (request.Image is { Data: not null })
        {
            var stream = request.Image.Data;
            stream.Seek(0, SeekOrigin.Begin);

            await fileManager.SaveAsync($"{request.Image.Id}_{request.Image.Name}", request.Image, cancellationToken)
                .ConfigureAwait(false);

            product.Image = request.Image;
        }

        await repos.CreateAsync(product, cancellationToken).ConfigureAwait(false);
        await repos.CommitAsync(cancellationToken).ConfigureAwait(false);

        return Result.Ok(product.Id);
    }
}