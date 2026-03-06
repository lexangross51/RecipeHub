using FluentResults;
using MediatR;
using RecipeHub.Application.Mapping.RecipeMapping.Create;
using RecipeHub.Application.Mapping.RecipeMapping.Update;
using RecipeHub.Domain.Models;

namespace RecipeHub.Application.Recipes.Commands.UpdateRecipe;

public class UpdateRecipeCommand : IRequest<Result>
{
    public required string Id { get; set; }

    public string NewName { get; set; } = default!;

    public TimeSpan? NewCookingTime { get; set; }

    public string? NewDescription { get; set; }

    public Image? NewRecipeImage { get; set; }

    public IEnumerable<CreateIngredientDto>? NewIngredients { get; set; }

    public IEnumerable<CreateRecipeStepDto>? NewSteps { get; set; }

    public IEnumerable<UpdateIngredientDto> EditedIngredients { get; set; } = default!;

    public IEnumerable<UpdateRecipeStepDto> EditedSteps { get; set; } = default!;
}