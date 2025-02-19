using FluentResults;
using MediatR;
using RecipeHub.Domain.Models;

namespace RecipeHub.Application.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<Result<string>>
{
    public required string Name { get; set; }

    public Image? Image { get; set; }
}