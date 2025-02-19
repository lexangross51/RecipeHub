using FluentValidation;

namespace RecipeHub.Application.Products.Commands.DeleteProduct;

internal class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator() => RuleFor(x => x.Id)
        .NotEmpty()
        .MinimumLength(1);
}