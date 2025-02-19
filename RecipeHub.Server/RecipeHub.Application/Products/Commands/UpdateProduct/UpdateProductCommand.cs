using FluentResults;
using MediatR;
using RecipeHub.Domain.Models;

namespace RecipeHub.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest<Result>
{
    public required string Id { get; set; }

    public string NewName { get; set; } = default!;

    public Image? NewImage { get; set; }
}