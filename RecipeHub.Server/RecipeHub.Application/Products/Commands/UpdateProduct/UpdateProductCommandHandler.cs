using FluentResults;
using FluentValidation;
using RecipeHub.Application.Common;
using RecipeHub.Domain.Services.Abstractions;
using RecipeHub.Domain.Storage.Abstractions;

namespace RecipeHub.Application.Products.Commands.UpdateProduct;

internal class UpdateProductCommandHandler(IProductRepository repos, IFileManager fileManager,
    IValidator<UpdateProductCommand> validator) : RequestHandler<UpdateProductCommand>(validator)
{
    protected override async Task<Result> HandleAsync(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await repos.GetAsync(request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (product == null)
        {
            string errorMessage = $"Не удалось найти продукт с id = {request.Id}";
            return Result.Fail(errorMessage);
        }

        product.Name = request.NewName;

        if (product.Image != null)
        {
            var path = product.Image.Path;

            if (path != null)
            {
                await fileManager.DeleteAsync(path, cancellationToken).ConfigureAwait(false);
            }

            if (request.NewImage is { Data: not null })
            {
                request.NewImage.Data.Seek(0, SeekOrigin.Begin);
                await fileManager.SaveAsync($"{request.NewImage.Id}_{request.NewImage.Name}", request.NewImage, cancellationToken)
                    .ConfigureAwait(false);

                product.Image = request.NewImage;
            }
        }

        await repos.UpdateAsync(product, cancellationToken).ConfigureAwait(false);
        await repos.CommitAsync(cancellationToken).ConfigureAwait(false);

        return Result.Ok();
    }
}