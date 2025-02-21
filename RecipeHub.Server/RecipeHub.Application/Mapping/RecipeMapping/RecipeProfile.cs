using AutoMapper;
using RecipeHub.Domain.Models;

namespace RecipeHub.Application.Mapping.RecipeMapping;

internal class RecipeProfile : Profile
{
    public RecipeProfile()
    {
        CreateMap<RecipeStep, RecipeStepDto>()
            .ForMember(dto => dto.Id,
            x => x.MapFrom(src => src.Id))
            .ForMember(dto => dto.Description,
            x => x.MapFrom(src => src.Description))
            .ForMember(dto => dto.ImageId,
            x => x.MapFrom(src => src.Image != null ? src.Image.Id : default));

        CreateMap<Ingredient, IngredientDto>()
            .ForMember(dto => dto.Id,
            opt => opt.MapFrom(src => src.Id))
            .ForMember(dto => dto.Name,
            opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dto => dto.Measure,
            opt => opt.MapFrom(src => src.Measure));

        CreateMap<Recipe, RecipeDto>()
            .ForMember(dto => dto.Name,
            x => x.MapFrom(src => src.Name))
            .ForMember(dto => dto.Description,
            x => x.MapFrom(src => src.Description))
            .ForMember(dto => dto.CookingTime,
            x => x.MapFrom(src => src.CookingTime))
            .ForMember(dto => dto.ImageId,
            x => x.MapFrom(src => src.RecipeImage != null ? src.RecipeImage.Id : default))
            .ForMember(dto => dto.Ingredients,
            x => x.MapFrom(src => src.Ingredients))
            .ForMember(dto => dto.RecipeSteps,
            x => x.MapFrom(src => src.Steps));

        CreateMap<Recipe, RecipeListItemDto>()
            .ForMember(dto => dto.ImageId,
            x => x.MapFrom(src => !string.IsNullOrEmpty(src.RecipeImageId) ? src.RecipeImageId : default))
            .ForMember(dto => dto.Ingredients,
            x => x.MapFrom(src => src.Ingredients.Select(i => i.Product.Name)));
    }
}