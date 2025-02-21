using AutoMapper;
using RecipeHub.Application.Recipes.Commands.CreateRecipe;
using RecipeHub.Domain.Models;
using RecipeHub.WebServer.DtoModels.Recipes.Create;

namespace RecipeHub.WebServer.DtoModels.Recipes.Mapping;

public class RecipeProfile : Profile
{
    public RecipeProfile()
    {
        CreateMap<IFormFile, Image>()
            .ForMember(f => f.Name,
            opt => opt.MapFrom(src => src.FileName))
            .ForMember(f => f.Data,
            opt => opt.MapFrom(src => ConvertToMemoryStream(src)));

        CreateMap<CreateRecipeStepDto, CreateRecipeStepDto>()
            .ForMember(d => d.Description,
            opt => opt.MapFrom(src => src.Description))
            .ForMember(d => d.StepImage,
            opt => opt.MapFrom(src => src.StepImage));

        CreateMap<CreateRecipeDto, CreateRecipeCommand>()
            .ForMember(cmd => cmd.Name,
            opt => opt.MapFrom(src => src.Name))
            .ForMember(cmd => cmd.Description,
            opt => opt.MapFrom(src => src.Description))
            .ForMember(cmd => cmd.RecipeImage,
            opt => opt.MapFrom(src => src.RecipeImage))
            .ForMember(cmd => cmd.CookingTime,
            opt => opt.MapFrom(src => src.CookingTime))
            .ForMember(cmd => cmd.Ingredients,
            opt => opt.MapFrom(src => src.Ingredients))
            .ForMember(cmd => cmd.Steps,
            opt => opt.MapFrom(src => src.Steps));
    }

    private static MemoryStream ConvertToMemoryStream(IFormFile file)
    {
        var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);
        memoryStream.Position = 0;
        return memoryStream;
    }
}