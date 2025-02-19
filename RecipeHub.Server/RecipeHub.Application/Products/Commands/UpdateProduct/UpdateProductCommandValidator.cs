using FluentValidation;

namespace RecipeHub.Application.Products.Commands.UpdateProduct;

internal class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .MinimumLength(1);

        RuleFor(x => x.NewName)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(128);
    }
}