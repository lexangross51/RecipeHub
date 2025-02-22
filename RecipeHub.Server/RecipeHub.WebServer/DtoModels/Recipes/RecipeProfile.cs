using AutoMapper;
using RecipeHub.WebServer.DtoModels.Recipes.Create;
using RecipeHub.WebServer.DtoModels.Recipes.Get;
using RecipeHub.WebServer.DtoModels.Recipes.Update;

namespace RecipeHub.WebServer.DtoModels.Recipes;

public class RecipeProfile : Profile
{
    public RecipeProfile()
    {
        // WebServer DTO -> Application DTO
        CreateMap<CreateRecipeStepDto, Application.Mapping.RecipeMapping.Create.CreateRecipeStepDto>();
        CreateMap<UpdateRecipeStepDto, Application.Mapping.RecipeMapping.Update.UpdateRecipeStepDto>();
        CreateMap<CreateRecipeDto, Application.Recipes.Commands.CreateRecipe.CreateRecipeCommand>();
        CreateMap<UpdateRecipeDto, Application.Recipes.Commands.UpdateRecipe.UpdateRecipeCommand>();

        // Application DTO -> WebServer DTO
        CreateMap<Application.Mapping.RecipeMapping.Get.GetRecipeStepDto, GetRecipeStepDto>();
        CreateMap<Application.Mapping.RecipeMapping.Get.GetRecipeDto, GetRecipeDto>();
        CreateMap<Application.Mapping.RecipeMapping.Get.GetRecipeListItemDto, GetRecipeListItemDto>();
    }
}