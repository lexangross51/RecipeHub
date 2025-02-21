using FluentResults;
using FluentValidation;
using RecipeHub.Application.Common;
using RecipeHub.Domain.Services.Abstractions;
using RecipeHub.Domain.Storage.Abstractions;

namespace RecipeHub.Application.Products.Commands.DeleteProduct;

internal class DeleteProductCommandHandler(IUnitOfWork uow, IValidator<DeleteProductCommand> validator, 
    IFileManager fileManager) : RequestHandler<DeleteProductCommand>(validator)
{
    protected override async Task<Result> HandleAsync(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var productRepos = uow.GetRepository<IProductRepository>();
        var imageRepos = uow.GetRepository<IImageRepository>();

        var product = await productRepos!.GetAsync(request.Id, cancellationToken).ConfigureAwait(false);

        if (product == null)
        {
            return Result.Ok();
        }

        await productRepos!.DeleteAsync(request.Id, cancellationToken).ConfigureAwait(false);

        if (!string.IsNullOrEmpty(product.ImageId))
        {
            await imageRepos!.DeleteAsync(product.ImageId, cancellationToken).ConfigureAwait(false);
        }

        await uow.CommitAsync(cancellationToken).ConfigureAwait(false);

        if (product.Image is { Path: not null })
        {
            await fileManager.DeleteAsync(product.Image.Path, cancellationToken).ConfigureAwait(false);
        }

        return Result.Ok();
    }
}