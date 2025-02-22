using AutoMapper;
using RecipeHub.Application.Mapping.RecipeMapping.Create;
using RecipeHub.Application.Mapping.RecipeMapping.Get;
using RecipeHub.Domain.Models;

namespace RecipeHub.Application.Mapping.RecipeMapping;

internal class RecipeProfile : Profile
{
    public RecipeProfile()
    {
        CreateMap<CreateRecipeStepDto, RecipeStep>()
            .ForMember(dto => dto.ImageId,
            opt => opt.MapFrom(src => src.StepImage  != null ? src.StepImage.Id : default));
        CreateMap<RecipeStep, GetRecipeStepDto>();
        CreateMap<Ingredient, GetIngredientDto>()
            .ForMember(dto => dto.Name,
            opt => opt.MapFrom(src => src.Product.Name));

        CreateMap<Recipe, GetRecipeDto>();
        CreateMap<Recipe, GetRecipeListItemDto>()
            .ForMember(dto => dto.Ingredients,
            x => x.MapFrom(src => src.Ingredients.Select(i => i.Product.Name)));
    }
}