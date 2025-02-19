using FluentResults;
using MediatR;
using RecipeHub.Application.Mapping.ImageMapping;

namespace RecipeHub.Application.Images.Queries;

public class GetImageQuery : IRequest<Result<ImageDto>>
{
    public required string Id { get; init; }
}