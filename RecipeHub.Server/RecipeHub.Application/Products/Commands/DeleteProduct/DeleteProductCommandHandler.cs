using FluentResults;
using FluentValidation;
using RecipeHub.Application.Common;
using RecipeHub.Domain.Storage.Abstractions;

namespace RecipeHub.Application.Products.Commands.DeleteProduct;

internal class DeleteProductCommandHandler(IProductRepository repos, IValidator<DeleteProductCommand> validator)
    : RequestHandler<DeleteProductCommand>(validator)
{
    protected override async Task<Result> HandleAsync(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await repos.DeleteAsync(request.Id, cancellationToken).ConfigureAwait(false);
        await repos.CommitAsync(cancellationToken).ConfigureAwait(false);
        return Result.Ok();
    }
}