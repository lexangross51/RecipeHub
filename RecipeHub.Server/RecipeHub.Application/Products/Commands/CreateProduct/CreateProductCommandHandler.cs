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
		try
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
                await fileManager.SaveAsync($"{request.Image.Id}_{request.Image.Name}", request.Image, cancellationToken)
                    .ConfigureAwait(false);

                product.Image = request.Image;
            }

            await repos.CreateAsync(product, cancellationToken).ConfigureAwait(false);
            await repos.CommitAsync(cancellationToken).ConfigureAwait(false);

            return Result.Ok(product.Id);
        }
		catch (Exception)
		{
            if (request.Image is { Path:not null })
            {
                await fileManager.DeleteAsync(request.Image.Path, cancellationToken).ConfigureAwait(false);
            }

			throw;
		}
    }
}