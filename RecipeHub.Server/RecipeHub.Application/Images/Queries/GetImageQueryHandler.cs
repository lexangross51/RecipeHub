using AutoMapper;
using FluentResults;
using FluentValidation;
using RecipeHub.Application.Common;
using RecipeHub.Application.Mapping.ImageMapping;
using RecipeHub.Domain.Storage.Abstractions;

namespace RecipeHub.Application.Images.Queries;

internal class GetImageQueryHandler(IImageRepository repos, IValidator<GetImageQuery> validator, IMapper mapper) 
    : RequestHandler<GetImageQuery, ImageDto>(validator)
{
    protected override async Task<Result<ImageDto>> HandleAsync(GetImageQuery request, CancellationToken cancellationToken)
    {
        var image = await repos.GetAsync(request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (image == null)
        {
            string errorMessage = $"Не удалось получить изображение с id = {request.Id}";
            return Result.Fail(errorMessage);
        }

        var vm = mapper.Map<ImageDto>(image);
        return Result.Ok(vm);
    }
}