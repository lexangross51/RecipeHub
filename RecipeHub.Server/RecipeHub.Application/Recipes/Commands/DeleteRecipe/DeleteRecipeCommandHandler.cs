using FluentResults;
using FluentValidation;
using RecipeHub.Application.Common;
using RecipeHub.Domain.Storage.Abstractions;

namespace RecipeHub.Application.Recipes.Commands.DeleteRecipe;

internal class DeleteRecipeCommandHandler(IRecipeRepository repos, IValidator<DeleteRecipeCommand> validator)
    : RequestHandler<DeleteRecipeCommand>(validator)
{
    protected override async Task<Result> HandleAsync(DeleteRecipeCommand request, CancellationToken cancellationToken)
    {
        await repos.DeleteAsync(request.Id, cancellationToken).ConfigureAwait(false);
        await repos.CommitAsync(cancellationToken).ConfigureAwait(false);

        return Result.Ok();
    }
}