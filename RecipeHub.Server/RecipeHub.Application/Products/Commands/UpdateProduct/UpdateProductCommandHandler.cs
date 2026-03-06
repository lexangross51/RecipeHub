using FluentResults;
using FluentValidation;
using RecipeHub.Application.Common;
using RecipeHub.Domain.Models;
using RecipeHub.Domain.Services.Abstractions;
using RecipeHub.Domain.Storage.Abstractions;

namespace RecipeHub.Application.Products.Commands.UpdateProduct;

internal class UpdateProductCommandHandler(IUnitOfWork uow, IFileManager fileManager,
    IValidator<UpdateProductCommand> validator) : RequestHandler<UpdateProductCommand>(validator)
{
    protected override async Task<Result> HandleAsync(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productRepos = uow.GetRepository<IProductRepository>();
        var imageRepos = uow.GetRepository<IImageRepository>();

        var product = await productRepos!.GetAsync(request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (product == null)
        {
            string errorMessage = $"Не удалось найти продукт с id = {request.Id}";
            return Result.Fail(errorMessage);
        }

        try
        {
            if (product.Name != request.NewName)
            {
                product.Name = request.NewName;
            }

            Image? imageToDelete = default;

            if (request.NewImage != null)
            {
                string fileName = $"{request.NewImage.Id}_{request.NewImage.Name}";
                await fileManager.SaveAsync(fileName, request.NewImage, cancellationToken).ConfigureAwait(false);
                await imageRepos!.CreateAsync(request.NewImage, cancellationToken).ConfigureAwait(false);

                imageToDelete = product.Image;
                product.Image = request.NewImage;
                product.ImageId = request.NewImage.Id;

                if (imageToDelete != null)
                {
                    await imageRepos!.DeleteAsync(imageToDelete.Id, cancellationToken).ConfigureAwait(false);
                }
            }

            await productRepos.UpdateAsync(product, cancellationToken).ConfigureAwait(false);
            await uow.CommitAsync(cancellationToken).ConfigureAwait(false);

            if (imageToDelete is { Path: not null })
            {
                await fileManager.DeleteAsync(imageToDelete.Path, cancellationToken).ConfigureAwait(false);
            }
        }
        catch (Exception)
        {
            if (request.NewImage is { Path: not null })
            {
                await fileManager.DeleteAsync(request.NewImage.Path, cancellationToken).ConfigureAwait(false);
            }

            throw;
        }
        
        return Result.Ok();
    }
}