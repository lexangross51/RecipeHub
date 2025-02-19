using FluentResults;
using MediatR;
using RecipeHub.Domain.Models;

namespace RecipeHub.Application.Recipes.Commands.UpdateRecipe;

public class UpdateRecipeCommand : IRequest<Result>
{
    public required string Id { get; set; }

    public string NewName { get; set; } = default!;

    public TimeSpan? NewCookingTime { get; set; }

    public string? NewDescription { get; set; }

    public Image? NewRecipeImage { get; set; }

    public IEnumerable<UpdateIngredientDto> NewIngredients { get; set; } = default!;

    public IEnumerable<UpdateRecipeStepDto> NewSteps { get; set; } = default!;
}