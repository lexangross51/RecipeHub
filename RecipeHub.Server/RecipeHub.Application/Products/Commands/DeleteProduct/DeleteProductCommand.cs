using FluentResults;
using MediatR;

namespace RecipeHub.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest<Result>
{
    public required string Id { get; set; }
}