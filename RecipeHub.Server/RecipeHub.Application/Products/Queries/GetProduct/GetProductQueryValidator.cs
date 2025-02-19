using FluentValidation;

namespace RecipeHub.Application.Products.Queries.GetProduct;

internal class GetProductQueryValidator : AbstractValidator<GetProductQuery>
{
    public GetProductQueryValidator() => RuleFor(x => x.Id)
        .NotEmpty()
        .MinimumLength(1);
}