using FluentValidation;

namespace RecipeHub.Application.Products.Commands.CreateProduct;

internal class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator() => RuleFor(x => x.Name)
        .NotEmpty()
        .MinimumLength(1)
        .MaximumLength(128);
}