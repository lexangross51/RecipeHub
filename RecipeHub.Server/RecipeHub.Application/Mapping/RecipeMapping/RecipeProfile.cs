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
            .ForMember(dto => dto.ImagePath,
            x => x.MapFrom(src => src.Image != null ? src.Image.Path : default));

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
            .ForMember(dto => dto.ImagePath,
            x => x.MapFrom(src => src.RecipeImage != null ? src.RecipeImage.Path : default))
            .ForMember(dto => dto.Ingredients,
            x => x.MapFrom(src => src.Ingredients))
            .ForMember(dto => dto.RecipeSteps,
            x => x.MapFrom(src => src.Steps));
    }
}