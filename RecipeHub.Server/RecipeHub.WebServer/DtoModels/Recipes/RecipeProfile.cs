using AutoMapper;
using RecipeHub.Application.Mapping.RecipeMapping;
using RecipeHub.Application.Recipes.Commands.CreateRecipe;
using RecipeHub.Application.Recipes.Commands.UpdateRecipe;
using RecipeHub.WebServer.DtoModels.Recipes.Create;
using RecipeHub.WebServer.DtoModels.Recipes.Get;
using RecipeHub.WebServer.DtoModels.Recipes.Update;

namespace RecipeHub.WebServer.DtoModels.Recipes;

public class RecipeProfile : Profile
{
    public RecipeProfile()
    {
        CreateMap<CreateStepDto, CreateRecipeStepDto>()
            .ForMember(d => d.Description,
            opt => opt.MapFrom(src => src.Description))
            .ForMember(d => d.StepImage,
            opt => opt.MapFrom(src => src.StepImage));

        CreateMap<UpdateStepDto, UpdateRecipeStepDto>()
            .ForMember(d => d.Id,
            opt => opt.MapFrom(src => src.Id))
            .ForMember(d => d.NewDescription,
            opt => opt.MapFrom(src => src.NewDescription))
        .ForMember(d => d.NewStepImage,
            opt => opt.MapFrom(src => src.NewStepImage));

        CreateMap<IngredientDto, GetIngredientDto>();
        CreateMap<RecipeStepDto, GetRecipeStepDto>();
        CreateMap<RecipeDto, GetRecipeDto>();

        CreateMap<Update.UpdateIngredientDto, Application.Recipes.Commands.UpdateRecipe.UpdateIngredientDto>()
            .ForMember(d => d.Id,
            opt => opt.MapFrom(src => src.Id))
            .ForMember(d => d.NewMeasure,
            opt => opt.MapFrom(src => src.NewMeasure));

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

        CreateMap<UpdateRecipeDto, UpdateRecipeCommand>()
            .ForMember(cmd => cmd.Id,
            opt => opt.MapFrom(src => src.Id))
            .ForMember(cmd => cmd.NewName,
            opt => opt.MapFrom(src => src.NewName))
            .ForMember(cmd => cmd.NewDescription,
            opt => opt.MapFrom(src => src.NewDescription))
            .ForMember(cmd => cmd.NewCookingTime,
            opt => opt.MapFrom(src => src.NewCookingTime))
            .ForMember(cmd => cmd.NewIngredients,
            opt => opt.MapFrom(src => src.NewIngredients))
            .ForMember(cmd => cmd.NewSteps,
            opt => opt.MapFrom(src => src.NewSteps))
            .ForMember(cmd => cmd.EditedSteps,
            opt => opt.MapFrom(src => src.EditedSteps))
            .ForMember(cmd => cmd.EditedIngredients,
            opt => opt.MapFrom(src => src.EditedIngredients));

        CreateMap<RecipeListItemDto, GetRecipeListDto>();
    }
}